namespace Persistence;
public class Staff{
    public int StaffID{get;set;}
    public string StaffName{get;set;} = "default";
    public string Address{get;set;} = "default";
    public string UserName{get;set;} = "default";
    public string Password{get;set;} = "default";
    public E.Staff.Role Role{get;set;}
}