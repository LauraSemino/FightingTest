using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Walk : ActionTask {

		public BBParameter<Vector3> input;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

            input.value = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
				input.value = Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
				input.value = Vector3.right;
            }
            if (Input.GetKey(KeyCode.W))
            {
				input.value = Vector3.up;	
            }
            if (Input.GetKey(KeyCode.S))
            {
				input.value = Vector3.down;
            }
        }
		

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}