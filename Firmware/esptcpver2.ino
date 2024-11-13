#include <ESP8266WiFi.h>
#include <WiFiClient.h>
#include <time.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <AHT20.h>
#include <RTClib.h>  // Thư viện hỗ trợ DS3231

// Pin definitions
#define BUTTON_SELECT_PIN D7 // Select and back button
#define BUTTON_DOWN_PIN D6   // Down button
#define BUTTON_STOP_PIN D8   // Stop button
#define LED_PIN D5           // Lamp pin
#define LED_PIN2 D3          // Connect LED pin
#define BUZZ_PIN D0          // Alarm buzz

// Wi-Fi credentials
const char* ssid = "inpy";
const char* password = "micropython1";

// OLED display and sensor objects
Adafruit_SSD1306 display(128, 64, &Wire, -1); 
AHT20 aht20;
RTC_DS3231 rtc;

// Server
WiFiServer server(8080);
WiFiClient client;

// Global variables
bool offlineMode = false;
bool rtcAvailable = false;
bool attemptedReconnect = false;
bool alarmActive = false;
bool lampActive = false;
bool buzzerActive = false;
bool SyncWithPCTime = false;

float temperature, humidity;
int selected = 0;
int entered = -1;
int day, month, year, hour, minute, second;
unsigned long previousMillis = 0;
const long interval = 500;  // OLED update interval
unsigned long lastDebounceTime_stop = 0;
const unsigned long debounceDelay_stop = 50;

char receivedChar;
String clientData;

// Timezone settings
const int GMTOffset = 25200;  // GMT Offset in seconds (7 hours)
const int daylightOffset = 0; // Daylight savings offset in seconds

// Enum for system modes
enum SystemMode {
  OFFLINE_MODE,
  WIFI_CONNECTED_MODE,
  RTC_CONNECTED_MODE
};

SystemMode currentMode = OFFLINE_MODE;

// Function to synchronize time with received PC time and offset
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
    timeinfo.tm_hour = hour ;
    timeinfo.tm_min = minute;
    timeinfo.tm_sec = second -10;

    // Chuyển đổi thành định dạng time_t
    time_t t = mktime(&timeinfo);

    // Điều chỉnh múi giờ theo offset nhận được
    t += offsetHours * 60 * 60;  // Chuyển đổi từ giờ sang giây

    // Cập nhật thời gian trên ESP
    timeval epoch = { t, 0 };
    settimeofday(&epoch, NULL);

}

// OLED initialization function
void setup_oled() {
  Wire.begin();
  if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C)) {
    for(;;);  // Freeze on failure
  }

  if (!aht20.begin()) {
    while (1);  // Freeze on failure
  }
  delay(2000);  // Short delay for startup
  display.clearDisplay();
}

// khai báo GPIO
void pinMode_init() {
  pinMode(LED_PIN, OUTPUT);
  pinMode(LED_PIN2, OUTPUT);
  pinMode(BUZZ_PIN, OUTPUT);
  pinMode(BUTTON_SELECT_PIN, INPUT_PULLUP);
  pinMode(BUTTON_DOWN_PIN, INPUT_PULLUP);
  pinMode(BUTTON_STOP_PIN, INPUT_PULLUP);
}

// Wi-Fi connection function
void wifiConnect() {
  if (WiFi.status() != WL_CONNECTED && !offlineMode && !attemptedReconnect) {
    display.clearDisplay();
    display.setTextSize(1);
    display.setCursor(0, 0);
    display.print("Connecting to WiFi...");
    display.display();

    WiFi.begin(ssid, password);
    unsigned long startAttemptTime = millis();

    while (WiFi.status() != WL_CONNECTED && millis() - startAttemptTime < 5000) {
      delay(500);
    }

    if (WiFi.status() == WL_CONNECTED) {
      display.clearDisplay();
      display.print("WiFi Connected");
      display.display();
      delay(1000);

      // Cấu hình thời gian từ máy chủ NTP
      configTime(GMTOffset, daylightOffset, "pool.ntp.org", "time.nist.gov");
      time_t now = time(nullptr);
      
      // Chờ cho đến khi nhận được thời gian hợp lệ (now >= 8 * 3600 * 2 tương ứng với thời gian từ năm 1970)
      while (now < 8 * 3600 * 2) {
        delay(500);
        now = time(nullptr);
      }

      // Thời gian đã đồng bộ thành công
      offlineMode = false;
      attemptedReconnect = false;

    } else {
      offlineMode = true;  // Chuyển sang chế độ offline nếu kết nối thất bại
    }
  }
}


// OLED update function
void updateOLED() {
  time_t rawtime = time(nullptr);
  struct tm* timeinfo; 
   
 
  if (rtcAvailable) {
    DateTime now = rtc.now();
    timeinfo->tm_year = now.year() ;
    timeinfo->tm_mon = now.month() ;
    timeinfo->tm_mday = now.day();
    timeinfo->tm_hour = now.hour();
    timeinfo->tm_min = now.minute();
    timeinfo->tm_sec = now.second();}
    else
    {
      timeinfo = SyncWithPCTime ? gmtime(&rawtime) : localtime(&rawtime);
    }

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
  
}

// Handling TCP client commands
void handleClientCommands() {
  client = server.available();
  if (client) {
    while (client.connected()) {
      digitalWrite(LED_PIN2, HIGH);
      client.print("D" + String(temperature) + "T" + String(humidity) + "H\n");
      updateOLED();

      while (client.available() > 0) {
        char receivedChar = client.read();
        clientData += receivedChar;
      }

      if (clientData == "ALARM#") {
        alarmActive = true;
      } else if (clientData == "LAMP#") {
        lampActive = true;
      } else if (clientData == "BUZZ#") {
        digitalWrite(BUZZ_PIN, HIGH);
      } else if (clientData == "OFF1#") {
        alarmActive = false;
        digitalWrite(BUZZ_PIN, LOW);
      } else if (clientData == "OFF2#") {
        lampActive = false;
        digitalWrite(LED_PIN, LOW);
      }
      else if (clientData == "ON2#") {
       digitalWrite(LED_PIN, HIGH);
  } else if (clientData.startsWith("GMT#")) {
    String receivedData = clientData.substring(4);
    syncTimeWithLocalTimeAndOffset(receivedData);
  }

      clientData = "";
      delay(500);
      digitalWrite(LED_PIN2, LOW);
    }
  }
}

// Handle system modes
void handleState() {
  switch (currentMode) {
    case OFFLINE_MODE:
      // Handle offline mode if needed
      break;
    case WIFI_CONNECTED_MODE:
      handleClientCommands();
      break;
    case RTC_CONNECTED_MODE:
      // Handle RTC mode if needed
      break;
  }
}

// Main loop
void loop() {
  unsigned long currentMillis = millis();
  if (currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis;
    updateOLED();
  }

  if (rtcAvailable) {
    currentMode = RTC_CONNECTED_MODE;
  } else if (WiFi.status() == WL_CONNECTED) {
    currentMode = WIFI_CONNECTED_MODE;
  } else {
    currentMode = OFFLINE_MODE;
  }

  handleState();
}

// Setup function
void setup() {
  Serial.begin(115200);
  setup_oled();
  pinMode_init();
  server.begin();
  rtcAvailable = rtc.begin();

  if (!rtcAvailable) {
    Serial.println("RTC not found");
  }

  wifiConnect();
}
