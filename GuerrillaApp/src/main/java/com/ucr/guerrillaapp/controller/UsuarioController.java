package com.ucr.guerrillaapp.controller;

import java.util.ArrayList;

import org.springframework.beans.factory.annotation.Autowired;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;



@Controller
public class UsuarioController {

	
		@RequestMapping(value = "/login", method = RequestMethod.GET)
		public String enviarCorreo() {
			return "login";
		}

}

