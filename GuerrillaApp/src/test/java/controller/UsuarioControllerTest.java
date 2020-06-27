package controller;

import static org.junit.Assert.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;

import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.test.context.junit4.SpringRunner;
import org.springframework.web.client.RestTemplate;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.ucr.guerrillaapp.domain.Guerrilla;
import com.ucr.guerrillaapp.domain.Recurso;

@SpringBootTest
public class UsuarioControllerTest {
	private Guerrilla guerrilla = new Guerrilla();
	private static String url = "http://74.207.226.124:5000/guerrilla";

	final private RestTemplate restTemplate = new RestTemplate();
	@Test
	public void login() throws IOException {
		/**String mail="dfonse11@gmail.com";
		// create headers
		HttpHeaders headers = new HttpHeaders();

		// set `Content-Type` and `Accept` headers
		headers.setContentType(MediaType.APPLICATION_JSON);
		headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));



		// build the request
		HttpEntity request = new HttpEntity(headers);

		// make an HTTP GET request with headers
		ResponseEntity<String> response = restTemplate.exchange(
		        url+"?email="+mail,
		        HttpMethod.GET,
		        request,
		        String.class,
		        1
		);
		
			  ObjectMapper mapper = new ObjectMapper();
			    JsonNode root = mapper.readTree(response.getBody());
			   
			    root.forEach(jsonObject -> {
			    	 guerrilla.setGuerrillaName(jsonObject.get("guerrillaName").asText());
			    	 
			    	 guerrilla.setRank(jsonObject.get("rank").asInt());
			    	 guerrilla.setFaction(jsonObject.get("faction").asText());
			              
			    });     
			   guerrilla.setEmail(mail);**/
			   
	}
	@Test
	public void myProfile() throws IOException {
		String name="los danieles";
		// create headers
		HttpHeaders headers = new HttpHeaders();

		// set `Content-Type` and `Accept` headers
		headers.setContentType(MediaType.APPLICATION_JSON);
		headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));



		// build the request
		HttpEntity request = new HttpEntity(headers);

		// make an HTTP GET request with headers
		ResponseEntity<String> response = restTemplate.exchange(
		        url+"?name="+name,
		        HttpMethod.GET,
		        request,
		        String.class,
		        1
		);
		
		ObjectMapper mapper = new ObjectMapper();
	    JsonNode root = mapper.readTree(response.getBody());
	      
	    JsonNode resources=root.findPath("resources"); 
	    System.out.print(resources.toString());
	    ArrayList<Recurso> resourcesGuerrilla=new ArrayList<>();
	    
	    
	    	Recurso oil=new Recurso();
	    	oil.setResource("oil");
	    	oil.setQuantity(resources.get("oil").asInt());
	    	
	    	Recurso money=new Recurso();
	    	money.setResource("money");
	    	money.setQuantity(resources.get("money").asInt());
	    	
	    	Recurso people=new Recurso();
	    	people.setResource("people");
	    	people.setQuantity(resources.get("people").asInt());
	    	
	    	resourcesGuerrilla.add(oil);
	    	resourcesGuerrilla.add(money);
	    	resourcesGuerrilla.add(people);
	  
	
		guerrilla.setResources(resourcesGuerrilla);
		System.out.print(guerrilla.getResources().toString());
	}

}
