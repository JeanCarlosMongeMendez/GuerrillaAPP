package controller;

import static org.junit.Assert.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

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
import com.ucr.guerrillaapp.domain.army;
import com.ucr.guerrillaapp.domain.buildings;

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
		/**String name="los danieles";
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
		System.out.print(guerrilla.getResources().toString());**/
	}
	@Test
	public void sign() throws IOException {
		/**String name="los danieles";
		String mail="dfonse11@gmail.com";
		String faction="Usa";
		// create headers
		HttpHeaders headers = new HttpHeaders();
		// set `content-type` header
		headers.setContentType(MediaType.APPLICATION_JSON);
		// set `accept` header
		headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

		// request body parameters
		Map<String, Object> map = new HashMap<>();
		map.put("guerrillaName", name);
		map.put("email", mail);
		map.put("faction", faction);
		System.out.print(map.toString());
		// build the request
		HttpEntity<Map<String, Object>> entity = new HttpEntity<>(map, headers);**/

	}
	@Test
	public void buyUnits() throws IOException {
	String name="los danieles";
		buildings Building=new buildings();
		Building.setBunkers(1);
		army Army=new army();
		// create headers
		HttpHeaders headers = new HttpHeaders();
		// set `content-type` header
		headers.setContentType(MediaType.APPLICATION_JSON);
		// set `accept` header
		headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

		// request body parameters
		Map<String, Object> map = new HashMap<>();
		map.put("army",Army.toString());
		map.put("buildings",Building.toString());
		System.out.print(map.toString());
		
		// build the request
		HttpEntity<Map<String, Object>> entity = new HttpEntity<>(map, headers);
	
		// send POST request
		restTemplate.put(url + "/" + name+"/units", entity, String.class);
	}

}
