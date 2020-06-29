package com.ucr.guerrillaapp;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

@SpringBootApplication
public class GuerrillaAppApplication {

	public static void main(String[] args) {
		SpringApplication.run(GuerrillaAppApplication.class, args);
		System.setProperty("com.sun.net.ssl.checkRevocation", "false");
	}

}
