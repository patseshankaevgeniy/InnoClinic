namespace Profiles.BLL.Common.Constants;

public class ExceptionConstants
{
    // Doctors
    public const string DoctorAlreadyExists = "Doctor with that name already exists.";
    public const string NotFoundDoctor = "There is no such doctor with that id";

    // Patients
    public const string PatientAlreadyExists = "Patient with that name already exists.";
    public const string NotFoundPatient = "There is no such patient with that id";

    // Receptionists
    public const string ReceptionistAlreadyExists = "Receptionist with that name already exists.";
    public const string NotFoundReceptionist = "There is no such receptionist with that id";
}
