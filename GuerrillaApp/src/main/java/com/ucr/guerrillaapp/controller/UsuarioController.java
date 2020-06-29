package com.ucr.guerrillaapp.controller;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.json.*;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpEntity;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.client.RestTemplate;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.ucr.guerrillaapp.domain.Guerrilla;
import com.ucr.guerrillaapp.domain.Recurso;
import com.ucr.guerrillaapp.domain.UnidadesDeBatalla;
import com.ucr.guerrillaapp.domain.army;
import com.ucr.guerrillaapp.domain.buildings;

@Controller
public class UsuarioController {

	private Guerrilla guerrilla = new Guerrilla();
	//private static String url = "http://74.207.226.124:5000/guerrilla";
	private static String url = "https://localhost:44304/guerrilla";
	final private RestTemplate restTemplate = new RestTemplate();



	@RequestMapping(value = "/login", method = RequestMethod.GET)
	public String login() {
		return "login";

	}

	@RequestMapping(value = "/login", method = RequestMethod.POST)
	public String login(@RequestParam("CorreoElectronico") String mail)
			throws JsonParseException, JsonMappingException, IOException {

		// create headers
		HttpHeaders headers = new HttpHeaders();

		// set `Content-Type` and `Accept` headers
		headers.setContentType(MediaType.APPLICATION_JSON);
		headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

		// build the request
		HttpEntity request = new HttpEntity(headers);

		// make an HTTP GET request with headers
		ResponseEntity<JSONArray> response = restTemplate.exchange(url + "?email=" + mail, HttpMethod.GET, request,
				JSONArray.class, 1);

		JSONArray res = response.getBody();
		ObjectMapper mapper = new ObjectMapper();
		JsonNode root = mapper.readTree(response.getBody().toString());

		root.forEach(jsonObject -> {
			guerrilla.setGuerrillaName(jsonObject.get("guerrillaName").asText());

			guerrilla.setRank(jsonObject.get("rank").asInt());
			guerrilla.setFaction(jsonObject.get("faction").asText());

		});
		guerrilla.setEmail(mail);

		return "login";
	}

	@RequestMapping(value = "/logout", method = RequestMethod.GET)
	public String logout() {
		guerrilla=new Guerrilla();
		return "login";

	}

	@RequestMapping(value = "/sign", method = RequestMethod.GET)
	public String sign() {
		return "sign";

	}

	@RequestMapping(value = "/sign", method = RequestMethod.POST)
	public String sign(@RequestParam("CorreoElectronico") String mail, @RequestParam("faccion") String faction,
			@RequestParam("NombreGuerrilla") String name) {

		guerrilla = new Guerrilla(0, name, faction, mail, new ArrayList<Recurso>(), new ArrayList<UnidadesDeBatalla>());

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

		// build the request
		HttpEntity<Map<String, Object>> entity = new HttpEntity<>(map, headers);

		// send POST request
		ResponseEntity<String> response = restTemplate.postForEntity(url + "/" + name, entity, String.class);


		return "sign";

	}

	@RequestMapping(value = "/myprofile", method = RequestMethod.GET)
	public String myProfile(Model model) throws IOException {

		if (guerrilla.getGuerrillaName() == null) {
			return "login";
		}
		// create headers
		HttpHeaders headers = new HttpHeaders();

		// set `Content-Type` and `Accept` headers
		headers.setContentType(MediaType.APPLICATION_JSON);
		headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

		// build the request
		HttpEntity request = new HttpEntity(headers);

		// make an HTTP GET request with headers
		// make an HTTP GET request with headers
		ResponseEntity<JSONObject> response = restTemplate.exchange(url + "/" + guerrilla.getGuerrillaName(),
				HttpMethod.GET, request, JSONObject.class, 1);

		JSONObject jsonObject = response.getBody();
		ObjectMapper mapper = new ObjectMapper();

		//JsonNode root = mapper.readTree(response.getBody());

		JSONObject rec = (JSONObject) jsonObject.get("resources");
		ArrayList<Recurso> resourcesGuerrilla = new ArrayList<>();

		Recurso oil = new Recurso();
		oil.setResource("oil");
		oil.setQuantity((Integer)rec.get("oil"));

		Recurso money = new Recurso();
		money.setResource("money");
		money.setQuantity((Integer) rec.get("money"));

		Recurso people = new Recurso();
		people.setResource("people");
		people.setQuantity((Integer) rec.get("people"));

		resourcesGuerrilla.add(oil);
		resourcesGuerrilla.add(money);
		resourcesGuerrilla.add(people);

		guerrilla.setResources(resourcesGuerrilla);
		model.addAttribute("resources", resourcesGuerrilla);
		model.addAttribute("nombre", guerrilla.getGuerrillaName());
		model.addAttribute("recurso", guerrilla.getResources());
		return "myprofile";
		
	}
	@RequestMapping(value = "/buyunits", method = RequestMethod.POST)
	public String buyUnits(@RequestParam("outputAssault") int outputAssault,@RequestParam("outputEngineer") int outputEngineer,
			@RequestParam("outputTank") int outputTank,@RequestParam("outputBunkers") int outputBunkers ) {
		buildings Building=new buildings();
		Building.setBunkers(outputBunkers);
		army Army=new army();
		Army.setAssault(outputAssault);
		Army.setEngineer(outputEngineer);
		Army.setTank(outputTank);
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
		
		
		// build the request
		HttpEntity<Map<String, Object>> entity = new HttpEntity<>(map, headers);
	
		// send POST request
		restTemplate.put(url + "/" + guerrilla.getGuerrillaName()+"/units", entity, String.class);

		return "myprofile";

	}
	@RequestMapping(value = "/ranking", method = RequestMethod.GET)
	public String ranking(Model model) throws IOException {
			if (guerrilla.getGuerrillaName() == null) {
				return "login";
			}
		// create headers
				HttpHeaders headers = new HttpHeaders();

				// set `Content-Type` and `Accept` headers
				headers.setContentType(MediaType.APPLICATION_JSON);
				headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

				// build the request
				HttpEntity request = new HttpEntity(headers);

				// make an HTTP GET request with headers
				ResponseEntity<String> response = restTemplate.exchange(url, HttpMethod.GET, request,
						String.class, 1);

				ObjectMapper mapper = new ObjectMapper();
				JsonNode root = mapper.readTree(response.getBody());
				List<Guerrilla> guerrillaList = new ArrayList<Guerrilla>();
				root.forEach(jsonObject -> {
					Guerrilla guerrillaTemp=new Guerrilla();
					String guerrillaName=jsonObject.get("guerrillaName").asText();
					if (!guerrillaName.equals(guerrilla.getGuerrillaName())) {
						guerrillaTemp.setGuerrillaName(guerrillaName);
						guerrillaTemp.setRank(jsonObject.get("rank").asInt());
						guerrillaTemp.setFaction(jsonObject.get("faction").asText());
						guerrillaList.add(guerrillaTemp);

					}
					
					
			
				});
				
		
		
		
		
		
		
		model.addAttribute("guerillaList", guerrillaList);
		return "ranking";
	}
	@RequestMapping(value = "/attack", method = RequestMethod.POST)
	public String attack(Model model,@RequestParam("targetGuerrillaName") String targetGuerrillaName) {
		
		// create headers
				HttpHeaders headers = new HttpHeaders();
				// set `content-type` header
				headers.setContentType(MediaType.APPLICATION_JSON);
				// set `accept` header
				headers.setAccept(Collections.singletonList(MediaType.APPLICATION_JSON));

				// request body parameters
				Map<String, Object> map = new HashMap<>();
				

				// build the request
				HttpEntity<Map<String, Object>> entity = new HttpEntity<>(map, headers);

				// send POST request
				ResponseEntity<String> response = restTemplate.postForEntity(url + "/attack/" + targetGuerrillaName+"?guerrillaSrc="+guerrilla.getGuerrillaName()
						, entity, String.class);
		
		model.addAttribute("targetGuerrillaName", targetGuerrillaName);
		
		return "result";

	}

	@RequestMapping(value = "/settings", method = RequestMethod.GET)
	public String settings() {

		return "settings";

	}

	@RequestMapping(value = "/settings", method = RequestMethod.POST)
	public String settings(@RequestParam("ipServidor") String ipServer) {
		url = ipServer + "/guerrilla";
		return "login";

	}
}
