using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionRecognizeLibrary
{
    public class OrionRecognizeZone
    {
        OrionRecognize orionRecognize;
        public OrionRecognizeZone(OrionRecognize orionRecognize)
        {
            this.orionRecognize = orionRecognize;
        }
        
        string text;
        public int BlkObj { get; set; }
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Name 
        {
            get
            {
                return text;
            }
            set
            {
                this.text = value;
                
            }
        }
        public string Value { get; set; }

        public void Update()
        {
            this.orionRecognize.UpdateZone(this);
        }
    }
}
