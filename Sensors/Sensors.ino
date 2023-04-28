#define Echo_EingangsPin 7
#define Trigger_AusgangsPin 8

long distance;
long duration;

int input;
int ledPin = 5;
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

  if (distance <= 150){
    Serial.println(distance);
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