package domain;

import java.util.ArrayList;

public class Guerrilla {
	private int idGuerrilla;
	private String nombreGuerrilla;
	private String correoGuerrilla;
	private ArrayList<Recurso> listaRecursos;
	private ArrayList<UnidadesDeBatalla> unidadesDeBatallas;

	public Guerrilla(int idGuerrilla, String nombreGuerrilla, String correoGuerrilla, ArrayList<Recurso> listaRecursos,
			ArrayList<UnidadesDeBatalla> unidadesDeBatallas) {
		super();
		setIdGuerrilla(idGuerrilla);
		setNombreGuerrilla(nombreGuerrilla);
		setCorreoGuerrilla(correoGuerrilla);
		setListaRecursos(listaRecursos);
		setUnidadesDeBatallas(unidadesDeBatallas);
	}

	public int getIdGuerrilla() {
		return idGuerrilla;
	}

	public void setIdGuerrilla(int idGuerrilla) {
		this.idGuerrilla = idGuerrilla;
	}

	public String getNombreGuerrilla() {
		return nombreGuerrilla;
	}

	public void setNombreGuerrilla(String nombreGuerrilla) {
		this.nombreGuerrilla = nombreGuerrilla;
	}

	public String getCorreoGuerrilla() {
		return correoGuerrilla;
	}

	public void setCorreoGuerrilla(String correoGuerrilla) {
		this.correoGuerrilla = correoGuerrilla;
	}

	public ArrayList<Recurso> getListaRecursos() {
		return listaRecursos;
	}

	public void setListaRecursos(ArrayList<Recurso> listaRecursos) {
		this.listaRecursos = listaRecursos;
	}

	public ArrayList<UnidadesDeBatalla> getUnidadesDeBatallas() {
		return unidadesDeBatallas;
	}

	public void setUnidadesDeBatallas(ArrayList<UnidadesDeBatalla> unidadesDeBatallas) {
		this.unidadesDeBatallas = unidadesDeBatallas;
	}

	@Override
	public String toString() {
		return "Guerrilla [idGuerrilla=" + idGuerrilla + ", nombreGuerrilla=" + nombreGuerrilla + ", correoGuerrilla="
				+ correoGuerrilla + ", listaRecursos=" + listaRecursos + ", unidadesDeBatallas=" + unidadesDeBatallas
				+ "]";
	}

}
