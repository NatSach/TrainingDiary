using System;
using System.ComponentModel;

namespace TrainingDiary.Models
{
    public class Workout : IEntity
    {
        public int Id { get; set; }
        public string Content {get; set;}
        public float OWB1 {get; set;}
        public float OWB2 {get; set;}
        public float WB3 {get; set;}
        public float ZB {get; set;}
        public float KR {get; set;}
        public float GL {get; set;}
        public float DL {get; set;}
        public float Rhythm {get; set;}

        [DisplayName("Relative speed")]
        public float RelativeSpeed {get; set;}
        [DisplayName("Max speed")]
        public float MaxSpeed {get; set;}
        public float Skip {get; set;}
        [DisplayName("Multiple jump")]
        public float MultipleJump {get; set;}
        public float MS {get; set;}
        public float BPG {get; set;}
        [DisplayName("KM start")]
        public float KMStart {get; set;}
        public float Scamper {get; set;}
        public string Comment {get; set;}
    }
}