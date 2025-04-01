using System;
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
    public WheelController wheelController;

    public void SetControllers()
    {
        wheelController = vehicleController.GetWheelController();
    }

    public virtual void Drive()
    {
        wheelController.ApplyWheelTurnAngle(vehicleController.GetLeftRight());
        wheelController.ApplyBrakeForce(vehicleController.GetBreak());
    }

    public static implicit operator Gear(string v)
    {
        throw new NotImplementedException();
    }
}

public class DriveGear : Gear
{
    public DriveGear(VehicleController vehicleController)
    {
        gear = GearType.DriveGear;
        this.vehicleController = vehicleController;
        SetControllers();
    }

    public override void Drive()
    {
        wheelController.ApplyAcceleration(vehicleController.GetAccelerate(),false);
        base.Drive();
    }
}

public class NeutralGear : Gear
{
    public NeutralGear(VehicleController vehicleController)
    {
        gear = GearType.NeutralGear;
        this.vehicleController = vehicleController;
        SetControllers();
    }

    public override void Drive()
    {
        wheelController.ApplyAcceleration(0f,false);
        base.Drive();
    }
}

public class ReverseGear : Gear
{
    public ReverseGear(VehicleController vehicleController)
    {
        gear = GearType.ReverseGear;
        this.vehicleController = vehicleController;
        SetControllers();
    }

    public override void Drive()
    {
        wheelController.ApplyAcceleration(vehicleController.GetAccelerate(),true);
        base.Drive();
    }
}

public class ParkingGear : Gear
{
    public ParkingGear(VehicleController vehicleController)
    {
        gear = GearType.ParkingGear;
        this.vehicleController = vehicleController;
        SetControllers();
    }

    public override void Drive() 
    {
        wheelController.ApplyAcceleration(0f,false);
        base.Drive();
    }
}
