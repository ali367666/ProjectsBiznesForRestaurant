namespace AlisRestaurant.Menus.HrMenus;

using AlisRestaurant.Services.HrService.DepartmentServices;
using AlisRestaurant.Services.HrService.PositionServices;

public class PositionMenu
{
    private readonly CreatePosition _createPosition = new CreatePosition();
    private readonly DeletePosition _deletePosition = new DeletePosition();
    private readonly UpdatePosition _updatePosition = new UpdatePosition();
    private readonly ListPosition _listPosition = new ListPosition();
    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Position Menyu ===");
            Console.WriteLine("1. Bütün Position-ları göstər");
            Console.WriteLine("2. Yeni Position əlavə et");
            Console.WriteLine("3. Position-u yenilə");
            Console.WriteLine("4. Position-u sil");
            Console.WriteLine("0. Geri qayıt");
            Console.Write("Seçiminizi edin: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var listPosition = new ListPosition();
                    listPosition.Execute();
                    break;
                case "2":
                    var createPosition = new CreatePosition();
                    createPosition.Execute();
                    break;
                case "3":
                    var updatePosition = new UpdatePosition();
                    updatePosition.Execute();
                    break;
                case "4":
                    var deletePosition = new DeletePosition();
                    deletePosition.Execute();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
