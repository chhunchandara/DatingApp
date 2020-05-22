namespace DatingApp.API.Models
{
    public class ActionInfo
    {
        public string Controller {get; set;}
        public string Action {get; set;}
        public string Area {get; set;}
        public ActionInfo(string Contrlller,string Action, string Area)
        {
            this.Controller = Controller;
            this.Action= Action;
            this.Area = Area;
            
        }
    }
}