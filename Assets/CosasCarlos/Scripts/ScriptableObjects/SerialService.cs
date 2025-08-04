using System;

[Serializable]
public class SerialService : InventoryItem<ServiceSO>
{

    public Boolean hasService = false;
    public double expTime;
    public SerialService(ServiceSO service) : base(service) {
        if(service.type == ItemTypeEnum.Licencias && service.time != -1)
        {
            expTime = service.time;
        }
        hasService = true;
    }

   
}

