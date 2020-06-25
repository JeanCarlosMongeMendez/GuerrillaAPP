package com.ucr.guerrillaapp.controller;

import java.util.ArrayList;

import org.springframework.beans.factory.annotation.Autowired;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;

import com.ucr.guerrillaapp.domain.Guerrilla;
import com.ucr.guerrillaapp.domain.Recurso;
import com.ucr.guerrillaapp.domain.UnidadesDeBatalla;



@Controller
public class UsuarioController {
	
		private Guerrilla guerrilla=new Guerrilla();
		private static final String URL = "https://localhost:44304/guerrilla/";
		@RequestMapping(value = "/login", method = RequestMethod.GET)
		public String login() {
			if (guerrilla.getCorreoGuerrilla()!=null) {
				return "myprofile";
			}else {
				return "login";
				
			}
				
			
			}
		
		@RequestMapping(value = "/login", method = RequestMethod.POST)
		public String login(Model model,@RequestParam("CorreoElectronico") String mail) {
			   guerrilla=new Guerrilla(0,"", mail, new ArrayList<Recurso>(),new ArrayList<UnidadesDeBatalla>());
			
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
		public String sign(Model model,@RequestParam("CorreoElectronico") String mail,@RequestParam("NombreGuerrilla") String name) {

			  guerrilla=new Guerrilla(0,name, mail, new ArrayList<Recurso>(),new ArrayList<UnidadesDeBatalla>());
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
			if (guerrilla.getCorreoGuerrilla()==null) {
				return "login";	
			}else{
			return "settings";
			}
		}
}

