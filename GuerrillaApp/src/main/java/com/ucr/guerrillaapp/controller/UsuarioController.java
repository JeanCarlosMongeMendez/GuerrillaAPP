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

@Controller
public class UsuarioController {

	private Guerrilla guerrilla = new Guerrilla();
	private static String url = "http://74.207.226.124:5000/guerrilla";
	// private static String url = "http://localhost:5000/guerrilla";
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
		ResponseEntity<String> response = restTemplate.exchange(url + "?email=" + mail, HttpMethod.GET, request,
				String.class, 1);

		ObjectMapper mapper = new ObjectMapper();
		JsonNode root = mapper.readTree(response.getBody());

		root.forEach(jsonObject -> {
			guerrilla.setGuerrillaName(jsonObject.get("guerrillaName").asText());

			guerrilla.setRank(jsonObject.get("rank").asInt());
			guerrilla.setFaction(jsonObject.get("faction").asText());

		});
		guerrilla.setEmail(mail);

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

		// check response
		if (response.getStatusCode() == HttpStatus.CREATED) {
			System.out.println("Request Successful");
			System.out.println(response.getBody());
		} else {
			System.out.println("Request Failed");
			System.out.println(response.getStatusCode());
		}
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
		ResponseEntity<String> response = restTemplate.exchange(url + "?name=" + guerrilla.getGuerrillaName(),
				HttpMethod.GET, request, String.class, 1);

		ObjectMapper mapper = new ObjectMapper();
		JsonNode root = mapper.readTree(response.getBody());

		JsonNode resources = root.findPath("resources");
		ArrayList<Recurso> resourcesGuerrilla = new ArrayList<>();

		Recurso oil = new Recurso();
		oil.setResource("oil");
		oil.setQuantity(resources.get("oil").asInt());

		Recurso money = new Recurso();
		money.setResource("money");
		money.setQuantity(resources.get("money").asInt());

		Recurso people = new Recurso();
		people.setResource("people");
		people.setQuantity(resources.get("people").asInt());

		resourcesGuerrilla.add(oil);
		resourcesGuerrilla.add(money);
		resourcesGuerrilla.add(people);

		guerrilla.setResources(resourcesGuerrilla);
		model.addAttribute("resources", resourcesGuerrilla);
		model.addAttribute("nombre", guerrilla.getGuerrillaName());
		model.addAttribute("recurso", guerrilla.getResources());
		return "myprofile";

	}

	@RequestMapping(value = "/ranking", method = RequestMethod.GET)
	public String ranking(Model model) throws IOException {
		
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
					guerrillaTemp.setGuerrillaName(jsonObject.get("guerrillaName").asText());

					guerrillaTemp.setRank(jsonObject.get("rank").asInt());
					guerrillaTemp.setFaction(jsonObject.get("faction").asText());
					guerrillaList.add(guerrillaTemp);

				});
				
		
		
		
		
		
		
		model.addAttribute("guerillaList", guerrillaList);
		return "ranking";
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
