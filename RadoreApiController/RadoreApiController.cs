namespace RadoreCaseRamazan.RadoreApiController;

public class RadoreApiController
{
    private static readonly Lazy<RadoreApiController> _instance = new Lazy<RadoreApiController>(() => new RadoreApiController());
    
    private RadoreApiController()
    {
        // Bağlantı ayarları veya diğer ilgili kodlar buraya gelebilir
    }

    public static RadoreApiController Instance => _instance.Value;
}