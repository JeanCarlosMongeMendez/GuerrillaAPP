package com.ucr.guerrillaapp.domain;

import java.util.ArrayList;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;


//@JsonIgnoreProperties(ignoreUnknown=true)
public class Guerrilla {
	private int idGuerrilla;
	//@JsonProperty("guerrillaName")
	private String guerrillaName;
	//@JsonProperty("rank")
	private int rank;
	//@JsonProperty("faction")
	private String faction;
	//@JsonProperty("email")
	private String email;
	//@JsonProperty("resources")
	private ArrayList<Recurso> resources;
	//@JsonProperty("army")
	private ArrayList<UnidadesDeBatalla> units;
	
	
	public Guerrilla(int idGuerrilla, String guerrillaName,String faction, String email, ArrayList<Recurso> resources,
			ArrayList<UnidadesDeBatalla> units) {
		super();
		setIdGuerrilla(idGuerrilla);
		setGuerrillaName(guerrillaName);
		setEmail(email);
		setResources(resources);
		setUnits(units);
		setFaction(faction);
	}

	public Guerrilla() {
		setResources(new ArrayList<Recurso>());
		setUnits(new ArrayList<UnidadesDeBatalla>());
		setEmail(null);
	}

	

	public int getIdGuerrilla() {
		return idGuerrilla;
	}

	public void setIdGuerrilla(int idGuerrilla) {
		this.idGuerrilla = idGuerrilla;
	}

	public String getGuerrillaName() {
		return guerrillaName;
	}

	public void setGuerrillaName(String guerrillaName) {
		this.guerrillaName = guerrillaName;
	}

	public String getFaction() {
		return faction;
	}

	public void setFaction(String faction) {
		this.faction = faction;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public ArrayList<Recurso> getResources() {
		return resources;
	}

	public void setResources(ArrayList<Recurso> resources) {
		this.resources = resources;
	}

	public ArrayList<UnidadesDeBatalla> getUnits() {
		return units;
	}

	public void setUnits(ArrayList<UnidadesDeBatalla> units) {
		this.units = units;
	}

	public int getRank() {
		return rank;
	}

	public void setRank(int rank) {
		this.rank = rank;
	}

	@Override
	public String toString() {
		return "Guerrilla [idGuerrilla=" + idGuerrilla + ", guerrillaName=" + guerrillaName + ", email="
				+ email + ", resources=" + resources + ", units=" + units
				+ "]";
	}

}
