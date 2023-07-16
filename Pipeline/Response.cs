using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSSP_example.Pipeline
{
    public class Response
    {
        public bool success;
        public string message;
        public float cashBack;
        
       /* public Response()
        {
          this.success = false;
          this.message = "";
        }*/
        public void set(bool success, string message)
        {
            this.success = success;
            this.message = message;
            
        }
        public void setCashback(float cashBack)
        { this.cashBack = cashBack; }
    }
}
