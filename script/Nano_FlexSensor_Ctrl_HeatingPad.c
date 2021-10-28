#define ENA 5
#define ENB 6
#define IN1 7
#define IN2 8
#define IN3 9
#define IN4 10
#define flex_pin1 A1
#define flex_pin2 A2

void setup() {
  // put your setup code here, to run once:
  pinMode(ENA, OUTPUT);
  pinMode(IN2, OUTPUT);
  pinMode(IN1, OUTPUT);
  pinMode(ENB, OUTPUT);
  pinMode(IN3, OUTPUT);
  pinMode(IN4, OUTPUT);
  pinMode(flex_pin1,INPUT);
  pinMode(flex_pin2,INPUT);

  Serial.begin(9600);
}

void loop() {
  
  //FlexSensor Part
  int flex_value1 = analogRead(flex_pin1);
  int flex_value2 = analogRead(flex_pin2);
  flex_value1 = map(flex_value1,0,1023,0,255);
  flex_value2 = map(flex_value2,0,1023,0,255);
  /////
  
  /////resize flexSensor value for Heating
  if(flex_value1<41){
    flex_value1=41;
  }else if(flex_value1>71){
    flex_value1=71;
  }
  if(flex_value2<53){
    flex_value2=53;
  }else if(flex_value2>70){
    flex_value2=70;
  }
  int heat_value1 = map(flex_value1,41,71,0,50);
  int heat_value2 = map(flex_value2,53,70,0,50);

  //Sending value to unity
  String sending_value = String(flex_value1) +","+ String(flex_value2) + "," + String(heat_value1) + "," + String(heat_value2);
  Serial.println(sending_value);
  
  

  ////Output for heating pad
  digitalWrite(IN1,HIGH);
  digitalWrite(IN2,LOW);
  digitalWrite(IN3,HIGH);
  digitalWrite(IN4,LOW);

  analogWrite(ENA, heat_value1);
  analogWrite(ENB, heat_value2);
  //////
}
