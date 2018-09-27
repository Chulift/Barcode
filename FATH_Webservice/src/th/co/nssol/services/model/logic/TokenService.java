package th.co.nssol.services.model.logic;

import com.microsoft.aad.adal4j.AuthenticationContext;
import com.microsoft.aad.adal4j.AuthenticationResult;
import com.microsoft.aad.adal4j.ClientAssertion;
import com.microsoft.aad.adal4j.ClientCredential;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
@Component
public class TokenService {
    //@Value("${abc.activedirectory.userName}")
    private String userName;
    //@Value("${abc.activedirectory.password}")
    private String password;
    //@Value("${abc.activedirectory.clientId}")
    private String clientId;
    //@Value("${abc.activedirectory.authEndPoint}")
    private String authEndPoint;
    //@Value("${abc.activedirectory.d365EndPoint}")
    private String d365EndPoint;
    //@Value("${abc.activedirectory.d365Path}")
    private String clientSecret;
    
    public TokenService(){
    	
    }
    
    public String getAccessToken() throws Exception {
        AuthenticationContext context = null;
        AuthenticationResult result = null;
        ExecutorService service = null;
        
        userName = "365admin@365nssol.onmicrosoft.com";
        password = "Nssol2017";
        
        clientId = "5eea10aa-9cc5-4b27-bf22-8f99fbec73ca";
        clientSecret = "srDmCTBets82g5/dhIyN4h0Hu6T5iPqb3BsGTGBYM7I=";
        
        authEndPoint = "https://login.microsoftonline.com/365nssol.onmicrosoft.com/";
        
        d365EndPoint = "https://usnconeboxax1aos.cloud.onebox.dynamics.com";
       
        ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);
        try {
            service = Executors.newFixedThreadPool(1);
            context = new AuthenticationContext(authEndPoint, false, service);
            Future<AuthenticationResult> future = context.acquireToken(d365EndPoint, clientCredential, null);
            result = future.get();
        } catch (Exception exception) {
        //LOGGER.error("Exception in TokenService in getAccessToken : " + exception.getMessage());
        } finally {
            service.shutdown();
        }
        return result.getAccessToken();
    }
    public String getURL() {
    StringBuilder urlBuilder = new StringBuilder(); 
    //LOGGER.info("Executing TokenService getURL()");
    urlBuilder.append(d365EndPoint);
    //urlBuilder.append(d365Path);
    return urlBuilder.toString();
    }
}
