using System.ComponentModel.DataAnnotations.Schema;

namespace CarShopCourseWork.Model.Data
{
    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        
        public string Color { get; set; }
        public decimal VolumeOfEngine { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsPreferredCar { get; set; }
        public int MakeId { get; set; }
        public virtual Make Make { get; set; }
        
        public decimal Price { get; set; }
        public decimal MaintenancePrice { get; set; }
        public int? Doors { get; set; }
        public int? Persons { get; set; }
        public int? LuggageBootSize { get; set; }
        public int? SafetyRatingNHTSA { get; set; }


        //Learning Model Cols 
        public string buying
        {
            get
            {
                switch (Price)
                {
                    case var val when val < 20000:
                        return "low";
                    case var val when val < 80000:
                        return "med";    
                    case var val when val < 150000:
                        return "high";
                    default:
                        return "vhigh";
                }
            }
        }
        public string maint
        {
            get
            {
                switch (MaintenancePrice)
                {
                    case var val when val < 200:
                        return "low";
                    case var val when val < 500:
                        return "med";    
                    case var val when val < 1000:
                        return "high";
                    default:
                        return "vhigh";
                }
            }
        }
        public float doors => Doors ?? 0;
        public float persons => Persons ?? 0;
        public string lug_boot
        {
            get
            {
                switch (LuggageBootSize)
                {
                    case var val when val < 201:
                        return "small";
                    case var val when val < 501:
                        return "med";
                    default:
                        return "big";
                }
            }
        }
        public string safety
        {
            get
            {
                switch (SafetyRatingNHTSA)
                {
                    case var val when val < 3 || val is null:
                        return "low";
                    case 4:
                        return "med";
                    default:
                        return "high";
                }
            }
        }
        
        [NotMapped]
        public string CarClass { get; set; }

        public string ValueForMoney
        {
            get
            {
                switch (CarClass)
                {
                    case "unacc":
                        return "Unacceptable";
                    case "acc":
                        return "Acceptable";
                    case "good":
                        return "Good";
                    case "vgood":
                        return "Very Good";
                    default:
                        return CarClass;
                }
            }
        }
        
        public string ValueForMoneyColor
        {
            get
            {
                switch (CarClass)
                {
                    case "unacc":
                        return "#f4511e";
                    case "acc":
                        return "#d4e157";
                    case "good":
                        return "#c8e6c9";
                    case "vgood":
                        return "#4caf50";
                    default:
                        return CarClass;
                }
            }
        }
    }
}
