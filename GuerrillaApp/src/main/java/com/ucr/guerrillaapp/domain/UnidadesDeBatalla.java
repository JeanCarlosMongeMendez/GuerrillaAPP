package com.ucr.guerrillaapp.domain;

public class UnidadesDeBatalla {
private int idUnidad;
private int cantidadUnidad;
private String nombreUnidad;
private float pillaje;
private float ataque;
private float defensa;
private float costoDinero;
private float costoPetroleo;
private float costoUnidades;

public UnidadesDeBatalla(int idUnidad, int cantidadUnidad, String nombreUnidad, float pillaje, float ataque,
		float defensa, float costoDinero, float costoPetroleo, float costoUnidades) {
	super();
	setIdUnidad(idUnidad);
	setCantidadUnidad(cantidadUnidad);
	setNombreUnidad(nombreUnidad);
	setPillaje(pillaje);
	setAtaque(ataque);
	setDefensa(defensa);
	setCostoDinero(costoDinero);
	setCostoPetroleo(costoPetroleo);
	setCostoUnidades(costoUnidades);
}
public int getIdUnidad() {
	return idUnidad;
}
public void setIdUnidad(int idUnidad) {
	this.idUnidad = idUnidad;
}
public int getCantidadUnidad() {
	return cantidadUnidad;
}
public void setCantidadUnidad(int cantidadUnidad) {
	this.cantidadUnidad = cantidadUnidad;
}
public String getNombreUnidad() {
	return nombreUnidad;
}
public void setNombreUnidad(String nombreUnidad) {
	this.nombreUnidad = nombreUnidad;
}
public float getPillaje() {
	return pillaje;
}
public void setPillaje(float pillaje) {
	this.pillaje = pillaje;
}
public float getAtaque() {
	return ataque;
}
public void setAtaque(float ataque) {
	this.ataque = ataque;
}
public float getDefensa() {
	return defensa;
}
public void setDefensa(float defensa) {
	this.defensa = defensa;
}
public float getCostoDinero() {
	return costoDinero;
}
public void setCostoDinero(float costoDinero) {
	this.costoDinero = costoDinero;
}
public float getCostoPetroleo() {
	return costoPetroleo;
}
public void setCostoPetroleo(float costoPetroleo) {
	this.costoPetroleo = costoPetroleo;
}
public float getCostoUnidades() {
	return costoUnidades;
}
public void setCostoUnidades(float costoUnidades) {
	this.costoUnidades = costoUnidades;
}
@Override
public String toString() {
	return "UnidadesDeBatalla [idUnidad=" + idUnidad + ", cantidadUnidad=" + cantidadUnidad + ", nombreUnidad="
			+ nombreUnidad + ", pillaje=" + pillaje + ", ataque=" + ataque + ", defensa=" + defensa + ", costoDinero="
			+ costoDinero + ", costoPetroleo=" + costoPetroleo + ", costoUnidades=" + costoUnidades + "]";
}
}
