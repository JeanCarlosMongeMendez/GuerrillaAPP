package com.ucr.guerrillaapp.controller;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import org.json.*;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.client.RestTemplate;

import com.ucr.guerrillaapp.domain.Guerrilla;
import com.ucr.guerrillaapp.domain.Recurso;
import com.ucr.guerrillaapp.domain.UnidadesDeBatalla;






@Controller
public class UsuarioController {
	
		private Guerrilla guerrilla=new Guerrilla();
		private static String url = "https://localhost:5001/guerrilla";
		final private RestTemplate restTemplate = new RestTemplate();
		
		@RequestMapping(value = "/login", method = RequestMethod.GET)
		public String login() {
			if (guerrilla.getCorreoGuerrilla()!=null) {
				return "myprofile";
			}else {
				return "login";
				
			}
				
			
			}
		
		@RequestMapping(value = "/login", method = RequestMethod.POST)
		public String login(@RequestParam("CorreoElectronico") String mail) {
			
			guerrilla=restTemplate.getForObject(url +"?email="+mail, Guerrilla.class);
			   	
			return "login";
		}
		
		@RequestMapping(value = "/sign", method = RequestMethod.GET)
		public String sign() {
			if (guerrilla.getCorreoGuerrilla()!=null) {
				return "myprofile";
			}else {
				return "sign";
			}
	
			
				}
		
		@RequestMapping(value = "/sign", method = RequestMethod.POST)
		public String sign(@RequestParam("CorreoElectronico") String mail,@RequestParam("faccion") String faction,@RequestParam("NombreGuerrilla") String name) {
		
			  guerrilla=new Guerrilla(0,name,faction, mail, new ArrayList<Recurso>(),new ArrayList<UnidadesDeBatalla>());
			

			// request body parameters
			Map<String, String> map = new HashMap<>();
			map.put("guerrillaName", name);
			map.put("email", mail);
			map.put("faction", faction);
			
			// send POST request
			ResponseEntity<Void> response = restTemplate.postForEntity(url+"/"+name, map, Void.class);

			// check response
			if (response.getStatusCode() == HttpStatus.OK) {
			    System.out.println("Request Successful");
			} else {
			    System.out.println("Request Failed");
			}
			  return "sign";
			
			
		}
		@RequestMapping(value = "/myprofile", method = RequestMethod.GET)
		public String myProfile() {
			if (guerrilla.getCorreoGuerrilla()==null) {
				return "login";	
			}else{
				return "myprofile";
			}
			
		}
		@RequestMapping(value = "/ranking", method = RequestMethod.GET)
		public String ranking() {
			if (guerrilla.getCorreoGuerrilla()==null) {
				return "login";	
			}else{
			return "ranking";
			}
		}
		@RequestMapping(value = "/settings", method = RequestMethod.GET)
		public String settings() {
			
			return "login";
			
		}
		@RequestMapping(value = "/settings", method = RequestMethod.POST)
		public String settings(@RequestParam("ipServidor") String ipServer) {
			url=ipServer+"/guerrilla";
			return "login";
			
		}
}

