using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    // Model widoku dla zarządzania zamówieniami
    public class OrderVM
    {
        // Lista elementów zamówienia
        public IEnumerable<CartModel>? OrderItems { get; set; }

    }
}
