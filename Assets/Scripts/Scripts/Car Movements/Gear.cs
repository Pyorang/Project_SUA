using UnityEngine;

public enum GearType
{
    DriveGear = 1, 
    NeutralGear = 2, 
    ReverseGear = 3, 
    ParkingGear = 4
}

public class Gear
{
    public GearType gear;
    public VehicleController vehicleController;

    public virtual void Drive()
    {
        vehicleController.GetWheelController().ApplyWheelTurnAngle(vehicleController.GetLeftRight());
        vehicleController.GetWheelController().ApplyBrakeForce(vehicleController.GetBreak());
        vehicleController.GetSteeringWheelController().UpdateSteeringWheel(vehicleController.GetLeftRight());
    }
}

public class DriveGear : Gear
{
    public DriveGear(VehicleController vehicleController)
    {
        gear = GearType.DriveGear;
        this.vehicleController = vehicleController;
    }

    public override void Drive()
    {
        vehicleController.GetWheelController().ApplyAcceleration(vehicleController.GetAccelerate(),false);
        base.Drive();
    }
}

public class NeutralGear : Gear
{
    public NeutralGear(VehicleController vehicleController)
    {
        gear = GearType.NeutralGear;
        this.vehicleController = vehicleController;
    }

    public override void Drive()
    {
        vehicleController.GetWheelController().ApplyAcceleration(0f,false);
        base.Drive();
    }
}

public class ReverseGear : Gear
{
    public ReverseGear(VehicleController vehicleController)
    {
        gear = GearType.ReverseGear;
        this.vehicleController = vehicleController;
    }

    public override void Drive()
    {
        vehicleController.GetWheelController().ApplyAcceleration(vehicleController.GetAccelerate(),true);
        base.Drive();

    }
}

public class ParkingGear : Gear
{
    public ParkingGear(VehicleController vehicleController)
    {
        gear = GearType.ParkingGear;
        this.vehicleController = vehicleController;
    }

    public override void Drive() 
    {
        vehicleController.GetWheelController().ApplyAcceleration(0f,false);
        base.Drive();
    }
}
