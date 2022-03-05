using ColossalFramework;
using TrafficManager.API.Traffic.Enums;
using TrafficManager.Manager.Impl;

namespace TrafficManager.Util.Extensions {
    public static class VehicleExtensions {
        private static Vehicle[] _vehicleBuffer = Singleton<VehicleManager>.instance.m_vehicles.m_buffer;

        /// <summary>Returns a reference to the vehicle instance.</summary>
        /// <param name="vehicleId">The ID of the vehicle instance to obtain.</param>
        /// <returns>The vehicle instance.</returns>
        public static ref Vehicle ToVehicle(this ushort vehicleId) => ref _vehicleBuffer[vehicleId];

        /// <summary>Returns a reference to the vehicle instance.</summary>
        /// <param name="vehicleId">The ID of the vehicle instance to obtain.</param>
        /// <returns>The vehicle instance.</returns>
        public static ref Vehicle ToVehicle(this uint vehicleId) => ref _vehicleBuffer[vehicleId];

        public static bool IsCreated(this ref Vehicle vehicle) =>
            vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created);

        public static bool IsParking(this ref Vehicle vehicle) =>
            vehicle.m_flags.IsFlagSet(Vehicle.Flags.Parking);

        /// <summary>
        /// Checks if the vehicle is Created, but not Deleted.
        /// </summary>
        /// <param name="vehicle">vehicle</param>
        /// <returns>True if the vehicle is valid, otherwise false.</returns>
        public static bool IsValid(this ref Vehicle vehicle) =>
            vehicle.m_flags.CheckFlags(
                required: Vehicle.Flags.Created,
                forbidden: Vehicle.Flags.Deleted);

        public static bool IsWaitingPath(this ref Vehicle vehicle) =>
            vehicle.m_flags.IsFlagSet(Vehicle.Flags.WaitingPath);

        /// <summary>Determines the <see cref="ExtVehicleType"/> for a vehicle.</summary>
        /// <param name="vehicle">The vehocle to inspect.</param>
        /// <returns>The extended vehicle type.</returns>
        public static ExtVehicleType ToExtVehicleType(this ref Vehicle vehicle) {
            VehicleInfo info = vehicle.Info;
            var vehicleId = info.m_instanceID.Vehicle;
            var vehicleAI = info.m_vehicleAI;
            // plane can have Emergency2 flag set in normal conditions
            var emergency = vehicle.m_flags.IsFlagSet(Vehicle.Flags.Emergency2) &&
                            info.m_vehicleType.IsFlagSet(VehicleInfo.VehicleType.Car);

            var ret = ExtVehicleManager.Instance.DetermineVehicleTypeFromAIType(
                vehicleId,
                vehicleAI,
                emergency);

            return ret ?? ExtVehicleType.None;
        }

    }
}
