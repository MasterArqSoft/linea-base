namespace microservice.dll.conection.Entities;

public partial class Partner
{
    public int PartnerId { get; set; }
    public string PartnerDescription { get; set; } = null!;
    public string PartnerDatabase { get; set; } = null!;
    public string UserBd { get; set; } = null!;
    public string PasswordBd { get; set; } = null!;
    public string ServerBd { get; set; } = null!;
    public int? PartnerCodeCardif { get; set; }
    public int? PartnerSubCodeCardif { get; set; }
}

