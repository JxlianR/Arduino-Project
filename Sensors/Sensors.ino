#define Echo_EingangsPin 7
#define Trigger_AusgangsPin 8

long distance;
long duration;

int photoresistor = A1;

int input;
//int ledPin = 5;
bool LED = false;

void setup() {
  // put your setup code here, to run once:
  pinMode(Trigger_AusgangsPin, OUTPUT);
  pinMode(Echo_EingangsPin, INPUT);
  Serial.begin(9600);

  //LED:
  /*pinMode(ledPin,OUTPUT);
  digitalWrite(ledPin,LOW);*/
}

void loop() {
  
  digitalWrite(Trigger_AusgangsPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(Trigger_AusgangsPin, LOW);

  duration = pulseIn(Echo_EingangsPin, HIGH);
  distance = duration / 58.2;

  //Serial.println(distance);

  int val = analogRead(photoresistor);
  float voltage = val * (5.0/1023) * 1000;
  float resistance = 10000 * (voltage / (5000.0 - voltage));
  if (voltage < 230)
  {
    Serial.println("LightsOn");
  }
  delay(100);

  

  // Change led:
  /*delay(100);
  if(Serial.available()>0)
  {
      input = Serial.read();
      digitalWrite(ledPin, input);
  }*/
}