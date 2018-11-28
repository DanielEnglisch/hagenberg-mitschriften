using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(PhoneTariff.MVC.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace PhoneTariff.MVC.App_Start {
    public static class RegisterClientValidationExtensions {
        public static void Start() {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}