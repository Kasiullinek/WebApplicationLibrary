using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    // Model widoku dla zarządzania danymi koszyka
    public class CartVM
    {
        // Lista elementów koszyka
        public IEnumerable<CartModel>? CartItems { get; set; }
    }
}
