#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <time.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <AHT20.h>
#include <RTClib.h>  
#define BUTTON_SELECT_PIN D7 // select and back button
#define BUTTON_DOWN_PIN D6   // down button
#define BUTTON_STOP_PIN D8   // stop buzz
#define LED_PIN D5           // lamp 
#define LED_PIN2 D3          // connect
#define BUZZ_PIN D0          // alarm buzz 

// Global variables
bool offlineMode = false;  // Biến này sẽ là true khi không có WiFi và RTC không kích hoạt
bool attemptedReconnect = false;  // Biến này để theo dõi xem người dùng đã thử kết nối lại chưa

char a;
String ClientData;
float temperature, humidity;
int current_screen = 0;  // 0 = menu, 1 = screenshot
int selected = 0, entered = -1;
unsigned long previousMillis = 0;
const long interval = 500;  // OLED update interval
bool buzzerActive = false;

//
WiFiServer server(8080);// mở port 8080
WiFiClient client;

// WiFi credentials
const char* ssid = "";
const char* password ="";

RTC_DS3231 rtc;
bool rtcAvailable = false;
bool alarmActive = false; // Trạng thái báo thức
bool lampActive = false;

// NTP Timezone settings
const int GMTOffset = 25200;  // GMT Offset in seconds (7 hours)
const int daylightOffset = 0; // Daylight savings offset in seconds

// OLED display
Adafruit_SSD1306 display(128, 64, &Wire, -1); 
AHT20 aht20;

// Button state
unsigned long lastDebounceTime_stop = 0;
const unsigned long debounceDelay_stop = 50;
int lastButtonState_stop = HIGH;

// Variables for time synchronization
int day, month, year, hour, minute, second;

bool SyncWithPCTime = false;

// Hàm đồng bộ thời gian với chuỗi GMT
void syncTimeWithLocalTimeAndOffset(String receivedData) {
    float offsetHours;
    SyncWithPCTime = true;
    // Phân tích chuỗi nhận được từ Winform (có thời gian và offset múi giờ)
    int parsed = sscanf(receivedData.c_str(), " %02d:%02d:%02d %d/%d/%d Offset: %f", 
                       &hour, &minute, &second, &day, &month, &year, &offsetHours);

    if (parsed != 7) {
        Serial.println("Failed to parse time data!");
        return;
    }

    // Tạo cấu trúc thời gian
    struct tm timeinfo;
    timeinfo.tm_year = year - 1900;
    timeinfo.tm_mon = month - 1;
    timeinfo.tm_mday = day;
    timeinfo.tm_hour = hour - 7;
    timeinfo.tm_min = minute;
    timeinfo.tm_sec = second;

    // Chuyển đổi thành định dạng time_t
    time_t t = mktime(&timeinfo);

    // Điều chỉnh múi giờ theo offset nhận được
    t += offsetHours * 60 * 60;  // Chuyển đổi từ giờ sang giây

    // Cập nhật thời gian trên ESP
    timeval epoch = { t, 0 };
    settimeofday(&epoch, NULL);

    
}

void setup_oled() {
  Wire.begin();
  if(!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) { 
    //Serial.println(F("SSD1306 allocation failed"));
    for(;;); // Freeze on failure
  }
  
  if(!aht20.begin()) {
    
    while (1);  // Freeze on failure
  }
  
  //Serial.println("AHT20 acknowledged.");
  delay(2000);  // Short delay for startup
  display.clearDisplay();
}

void pinMode_init() {
  pinMode(LED_PIN, OUTPUT);
  pinMode(LED_PIN2, OUTPUT);
  pinMode(BUZZ_PIN, OUTPUT);
  pinMode(BUTTON_SELECT_PIN, INPUT_PULLUP);
  pinMode(BUTTON_DOWN_PIN, INPUT_PULLUP);
  pinMode(BUTTON_STOP_PIN, INPUT_PULLUP);
}


void wifiConnect() {
  if (WiFi.status() != WL_CONNECTED && !offlineMode && !attemptedReconnect) {
    // Hiển thị thông báo kết nối WiFi trên OLED chỉ khi người dùng chưa thử reconnect
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE);
    display.setCursor(0, 0);
    display.print("Connecting to WiFi...");
    display.display();
    
    WiFi.begin(ssid, password);
    Serial.println("");
    
    unsigned long startAttemptTime = millis();
    while (WiFi.status() != WL_CONNECTED && millis() - startAttemptTime < 5000) {  // Chờ 5 giây để kết nối Wi-Fi
      delay(500);
      Serial.print(".");
    }

    if (WiFi.status() == WL_CONNECTED) {
      Serial.println("\nConnected to WiFi");
      Serial.print("IP Address: ");
      Serial.println(WiFi.localIP());
      
      // Cấu hình NTP và đồng bộ thời gian
      configTime(GMTOffset, daylightOffset, "pool.ntp.org", "time.nist.gov");
      time_t now = time(nullptr);
      while (now < 8 * 3600 * 2) { // Chờ thời gian được đặt
        delay(500);
        now = time(nullptr);
      }
      
      Serial.println("Time synchronized");

      // Hiển thị thông báo đồng bộ thành công
      display.clearDisplay();
      display.setTextSize(1);
      display.setCursor(0, 0);
      display.print("Time synchronized");
      display.display();
      delay(1000);  // Hiển thị thông báo trong 1 giây
      
      // Reset trạng thái offline sau khi kết nối thành công
      offlineMode = false;
      attemptedReconnect = false;  // Đặt lại sau khi kết nối thành công
    } else if(rtcAvailable){
      if (!rtc.begin()) {
    Serial.println("Couldn't find RTC");
    
    // Hiển thị thông báo nếu không tìm thấy RTC
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE);
    display.setCursor(0, 0);
    display.println("No RTC!");
    display.display();
    
    rtcAvailable = false;  // RTC không khả dụng
  } else {
    rtcAvailable = true;
    if (rtc.lostPower()) {
      Serial.println("RTC lost power, setting time!");
      rtc.adjust(DateTime(F(__DATE__), F(__TIME__)));
    }
    }}
    else{
      Serial.println("Failed to connect WiFi, going offline mode");
      offlineMode = true;  // Vào chế độ offline nếu không kết nối được WiFi
    }
  }
}


void updateOLED() {
  // Lấy thời gian hiện tại từ hệ thống
  time_t rawtime = time(nullptr);
  struct tm* timeinfo;
  if(SyncWithPCTime == true){
    timeinfo = gmtime(&rawtime);
  } else {
    timeinfo = localtime(&rawtime);
  }


   if (rtcAvailable) {
    // Nếu mất WiFi, lấy thời gian từ RTC
    DateTime now = rtc.now();
    timeinfo = localtime(&rawtime);
    timeinfo->tm_year = now.year() - 1900;
    timeinfo->tm_mon = now.month() - 1;
    timeinfo->tm_mday = now.day();
    timeinfo->tm_hour = now.hour();
    timeinfo->tm_min = now.minute();
    timeinfo->tm_sec = now.second();
  }

  // Đọc cảm biến AHT20 nếu có sẵn
  if (aht20.available()) {
    temperature = aht20.getTemperature();
    humidity = aht20.getHumidity();
  }

  // Xử lý nút nhấn để chuyển đổi qua các tùy chọn menu
  int select = digitalRead(BUTTON_DOWN_PIN);
  if (select == LOW) {
    selected = (selected + 1) % 3;  // Vòng qua các tùy chọn
    entered = -1;  // Trở về menu khi nhấn nút
    delay(200);    // Debounce delay
  }

  // Xử lý nút nhấn để vào một tùy chọn menu
  int enter = digitalRead(BUTTON_SELECT_PIN);
  if (enter == LOW) {
    if (entered == -1) {
      entered = selected;
      delay(200); // Debounce delay
    } else if (entered == selected) {
      entered = -1;
      delay(200); // Debounce delay
    }
  }

  // Menu Options
  const char *options[3] = {" Introduction ", " Alarm ", " Temp "};

  display.clearDisplay();
  if (entered == -1) {
    // Hiển thị thời gian hiện tại
    display.setTextSize(2);
    display.setTextColor(WHITE);
    display.setCursor(0, 0);
    display.printf("%02d:%02d:%02d\n", timeinfo->tm_hour, timeinfo->tm_min, timeinfo->tm_sec);

    // Hiển thị ngày tháng
    display.setTextSize(1);
    display.setCursor(0, 20);
    display.printf("%02d/%02d/%04d\n", timeinfo->tm_mday, timeinfo->tm_mon + 1, timeinfo->tm_year + 1900);

    // Kiểm tra kết nối WiFi và hiển thị IP hoặc "No Connection"
    display.setTextSize(1);
    display.setCursor(0, 30);
    if (WiFi.status() == WL_CONNECTED) {
      display.printf("IP: %s", WiFi.localIP().toString().c_str());
    } else {
      display.print("No Connection");
    }

    // Hiển thị menu
    display.setTextSize(1);
    display.setTextColor(SSD1306_WHITE);
    display.setCursor(0, 40);

    for (int i = 0; i < 3; i++) {
      if (i == selected) {
        display.setTextColor(SSD1306_BLACK, SSD1306_WHITE);
      } else {
        display.setTextColor(SSD1306_WHITE);
      }
      display.println(options[i]);
    }

  } 
  else if (entered == 0) {
    // Hiển thị thông tin giới thiệu
    display.setTextSize(1);
    display.setTextColor(WHITE);
    display.setCursor(0, 0);
    display.println(F("Du an \"Dong ho"));
    display.println(F("thong minh\" nhom 2"));
    display.println(F("DHDTVT17C thuc hien"));
    display.println(F("GVHD Mac Duc Dung."));
    display.println(F("Ket noi Wifi (TCP),"));
    display.println(F("dieu khien bang"));
    display.println(F("Winform tren PC."));
  } 
  else if (entered == 1) {
    // Chế độ báo động với khả năng bật/tắt
    display.setTextSize(2);
    display.setTextColor(WHITE);
    display.setCursor(0, 0);
    
    // Hiển thị trạng thái báo động
    if (alarmActive == true ) {
      display.println(F("Alarm ON"));
    } else {
      display.println(F("Alarm OFF"));
    }

    if (lampActive == true ) {
      display.println(F("Lamp ON"));
    } else {
      display.println(F("Lamp OFF"));
    }

    
  }
  else if (entered == 2) {
    // Hiển thị nhiệt độ và độ ẩm
    display.setTextSize(2);
    display.setTextColor(WHITE);
    display.setCursor(0, 0);
    display.println(F("Temp: "));
    display.println(temperature);
    display.println(F("Humi: "));
    display.println(humidity);
  }

  display.display();
  
  // Debug: In thời gian ra Serial
  Serial.printf("Time: %02d:%02d:%02d\n", timeinfo->tm_hour, timeinfo->tm_min, timeinfo->tm_sec);
}

void handleLED() {
  
  // Xử lý lệnh từ client
  while (client.available() > 0) {
    a = client.read();
    ClientData += a;
  }

  // Các lệnh điều khiển khác từ client
   if (ClientData == "ALARM#") {
    alarmActive = true;
  } 

  if (ClientData == "LAMP#") {
   lampActive = true;
  }


  if (ClientData == "BUZZ#") {
    digitalWrite(BUZZ_PIN, HIGH);
  } else if (ClientData == "OFF1#") {
    alarmActive = false;
    digitalWrite(BUZZ_PIN, LOW);
  }

  
  if (ClientData == "ON2#") {
    digitalWrite(LED_PIN, HIGH);
  } else if (ClientData == "OFF2#") {
    lampActive = false;
    digitalWrite(LED_PIN, LOW);
  }

  //nhận chuỗi ký tự thời gian từ pc gửi đến thiết bị
  if (ClientData.startsWith("GMT#")) {
    String receivedData = ClientData.substring(4);
    syncTimeWithLocalTimeAndOffset(receivedData);
  }

  ClientData = ""; //reset chuỗi ký tự 
}



void setup() {
  Serial.begin(115200);
  setup_oled();       // Khởi tạo OLED trước
  pinMode_init();     // Khởi tạo các chân GPIO
  server.begin();     // Khởi động server TCP
  wifiConnect();  // Kết nối WiFi ban đầu
}

void loop() {
  // Nếu WiFi không kết nối và không có RTC, hiển thị thông báo "No RTC"
  if (WiFi.status() != WL_CONNECTED && !rtcAvailable && offlineMode) {
    display.clearDisplay();
    display.setTextSize(1);
    display.setTextColor(WHITE);
    display.setCursor(0, 0);
    display.println("No RTC!");
    display.println("Press D8 to retry");
    display.display();
    
    // Kiểm tra nút nhấn D8 để thử kết nối lại WiFi
    int stopButtonState = digitalRead(BUTTON_STOP_PIN);
    if (stopButtonState == LOW) {
      offlineMode = false;           // Thoát khỏi chế độ offline để thử kết nối lại
      attemptedReconnect = true;     // Đánh dấu rằng đã thử kết nối lại
      wifiConnect();                 // Gọi hàm wifiConnect để thử lại
      delay(500);                    // Debounce cho nút nhấn
    }
  } else if (WiFi.status() == WL_CONNECTED || rtcAvailable) {
    // Hệ thống hoạt động bình thường, cập nhật OLED và xử lý các chức năng khác
    unsigned long currentMillis = millis();
    if (currentMillis - previousMillis >= interval) {
      previousMillis = currentMillis;
      updateOLED();
    }

    client = server.available();
    if (client) {
      while (client.connected()) {
        digitalWrite(LED_PIN2, HIGH);
        client.print("D" + String(temperature) + "T" + String(humidity) + "H\n");
        updateOLED();
        while (client.available() > 0) {
          handleLED();
        }
        delay(1000);
      }
      digitalWrite(LED_PIN2, LOW);
      client.stop();
    }
  }
}