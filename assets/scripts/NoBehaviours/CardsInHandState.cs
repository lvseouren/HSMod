using UnityEngine;
using System.Collections;

public static class CardsInHandState {

	public  enum States {
		DRAWING ,
		NOT_MY_TURN , 
		HOLDING_CARD ,
		NOT_HOLDING_CARD 
	}


	public static States currentState = States.NOT_HOLDING_CARD;




}
