using UnityEngine;

public enum GearType
{
    DriveGear = 1, NeutralGear = 2, ReverseGear = 3, ParkingGear = 4
}

public interface Gear
{
    public void Drive();
}

public class DriveGear : Gear
{
    public const float defaultSpeed = 100f;
    public GearType gear = GearType.DriveGear;
    public VehicleController vehicleController;

    public DriveGear(VehicleController vehicleController)
    {
        this.vehicleController = vehicleController;
    }

    public void Drive()
    {
        vehicleController.GetWheelController().ApplyAcceleration(defaultSpeed + vehicleController.GetAccelerate());
        vehicleController.GetWheelController().ApplyWheelTurnAngle(vehicleController.GetLeftRight());
        vehicleController.GetWheelController().ApplyBreakForce(vehicleController.GetBreak());

        vehicleController.GetSteeringWheelController().UpdateSteeringWheel(vehicleController.GetLeftRight());
    }
}

public class NeutralGear : Gear
{
    public GearType gear = GearType.NeutralGear;
    public VehicleController vehicleController;

    public NeutralGear(VehicleController vehicleController)
    {
        this.vehicleController = vehicleController;
    }

    public void Drive()
    {
        vehicleController.GetWheelController().ApplyAcceleration(0f);
        vehicleController.GetWheelController().ApplyWheelTurnAngle(vehicleController.GetLeftRight());
        vehicleController.GetWheelController().ApplyBreakForce(vehicleController.GetBreak());

        vehicleController.GetSteeringWheelController().UpdateSteeringWheel(vehicleController.GetLeftRight());
    }
}

public class ReverseGear : Gear
{
    public const float defaultSpeed = 100f;
    public GearType gear = GearType.ReverseGear;
    public VehicleController vehicleController;

    public ReverseGear(VehicleController vehicleController)
    {
        this.vehicleController = vehicleController;
    }

    public void Drive()
    {
        vehicleController.GetWheelController().ApplyAcceleration(-defaultSpeed-vehicleController.GetAccelerate());
        vehicleController.GetWheelController().ApplyWheelTurnAngle(vehicleController.GetLeftRight());
        vehicleController.GetWheelController().ApplyBreakForce(vehicleController.GetBreak());

        vehicleController.GetSteeringWheelController().UpdateSteeringWheel(vehicleController.GetLeftRight());

    }
}

public class ParkingGear : Gear
{
    public const float defaultSpeed = 100f;
    public GearType gear = GearType.ParkingGear;
    public VehicleController vehicleController;

    public ParkingGear(VehicleController vehicleController)
    {
        this.vehicleController = vehicleController;
    }

    public void Drive() 
    {
        vehicleController.GetWheelController().ApplyAcceleration(0f);
        vehicleController.GetWheelController().ApplyWheelTurnAngle(vehicleController.GetLeftRight());

        vehicleController.GetSteeringWheelController().UpdateSteeringWheel(vehicleController.GetLeftRight());
    }
}
