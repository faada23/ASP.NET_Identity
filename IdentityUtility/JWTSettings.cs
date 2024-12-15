namespace IdentityUtility.JWT;

public class JWTSettings{
    public TimeSpan Expires {get;set;}
    public string SecretKey{get;set;} =null!;
}