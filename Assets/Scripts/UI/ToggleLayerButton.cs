using CrossComm.Framework.ScriptableObjects;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleLayerButton : MonoBehaviour
{
	private TextMeshPro description;

	void Start() {
		description = GameObject.Find("Description").GetComponent<TextMeshPro>();
	}

	#region Private Fields
	//Serialized
	[Tooltip("Layer SO that is connected to Layer")]
	[SerializeField] private LayerSO _layerToToggle;
	[Tooltip("Event to send toggle event")]
	[SerializeField] private EventChannelSO _OnSetLayerStateEvent;
	//Non-Serialized
	//All layers toggled on by default
	private bool _isToggled = true;
	#endregion Private Fields

	#region Public Fields
	#endregion Public Fields

	#region Monobehavior Methods
	#endregion Monobehavior Methods

	#region Private Methods
	#endregion Private Methods

	#region Public Methods

	/// <summary>
	/// Sets state of UI and layer
	/// </summary>
	/// <param name="i_state"></param>
	public void SetState(bool i_state)
	{
		_isToggled = i_state;
		_OnSetLayerStateEvent.RaiseEvent(_layerToToggle, _isToggled);
	}

	#endregion Public Methods

	#region Coroutines
	#endregion Coroutines

	#region Events

	/// <summary>
	/// Callback for button clicked
	/// </summary>
	public void OnClick()
	{
		_isToggled = !_isToggled;
		_OnSetLayerStateEvent.RaiseEvent(_layerToToggle, _isToggled);
		if (_layerToToggle.name == "Skin") {
			description.text = "The integument of an animal (such as a fur-bearing mammal or a bird) separated from the body usually with its hair or feathers";
		}
		else if (_layerToToggle.name == "Fat") {
			description.text = "A natural oily or greasy substance occurring in animal bodies, especially when deposited as a layer under the skin or around certain organs";
		}
		else if (_layerToToggle.name == "Muscle") {
			description.text = "A body tissue consisting of long cells that contract when stimulated and produce motion";
		}
		else if (_layerToToggle.name == "Skeleton") {
			description.text = "The bony or more or less cartilaginous framework supporting the soft tissues and protecting the internal organs of a vertebrate";
		}
		else if (_layerToToggle.name == "Circulatory") {
			description.text = "The system that circulates blood and lymph through the body, consisting of the heart, blood vessels, blood, lymph, and the lymphatic vessels and glands";
		}
		else if (_layerToToggle.name == "Nervous") {
			description.text = "The bodily system that in vertebrates is made up of the brain and spinal cord, nerves, ganglia, and parts of the receptor organs and that receives and interprets stimuli and transmits impulses to the effector organs";
		}
		else if (_layerToToggle.name == "Urinary") {
			description.text = "The organs that make urine and remove it from the body, including the kidneys, ureters, blader and urethra";
		}
		else if (_layerToToggle.name == "Venous") {
			description.text = "Any of the tubular branching vessels that carry blood from the capillaries toward the heart";
		}
		else if (_layerToToggle.name == "Arterial") {
			description.text = "Any of the tubular branching muscular- and elastic-walled vessels that carry blood from the heart through the body";
		}
		else if (_layerToToggle.name == "Spinal Cord") {
			description.text = "The cylindrical bundle of nerve fibers and associated tissue which is enclosed in the spine and connects nearly all parts of the body to the brain, with which it forms the central nervous system";
		}
		else if (_layerToToggle.name == "Respiratory") {
			description.text = "A system of organs functioning in respiration and in humans consisting especially of the nose, nasal passages, pharynx, larynx, trachea, bronchi, and lungs";
		}
		else if (_layerToToggle.name == "Gastrointestinal") {
			description.text = "The track or passageway of the digestive system that leads from the mouth to the anus, including all the major organs in the digestive system";
		}
		else if (_layerToToggle.name == "Gastrointestinal") {
			description.text = "The track or passageway of the digestive system that leads from the mouth to the anus, including all the major organs in the digestive system";
		}
		else if (_layerToToggle.name == "Liver") {
			description.text = "A large very vascular glandular organ of vertebrates that secretes bile and causes important changes in many of the substances contained in the blood (as by converting sugars into glycogen which it stores up until required and by forming urea)";
		}
		else if (_layerToToggle.name == "Lungs") {
			description.text = "One of the usually paired compound saccular thoracic organs that constitute the basic respiratory organs of an air-breathing vertebrate";
		}
		else if (_layerToToggle.name == "Stomach") {
			description.text = "A saclike expansion of the digestive tract of a vertebrate that is located between the esophagus and duodenum and typically consists of a simple often curved sac with an outer serous covering, a strong muscular wall that contracts rhythmically, and an inner mucous membrane lining that contains gastric glands";
		}
		else if (_layerToToggle.name == "Heart") {
			description.text = "A hollow muscular organ of vertebrate animals that by its rhythmic contraction acts as a force pump maintaining the circulation of the blood";
		}
		else if (_layerToToggle.name == "Brain") {
			description.text = "An organ of soft nervous tissue contained in the skull of vertebrates, functioning as the coordinating center of sensation and intellectual and nervous activity.";
		}
		else if (_layerToToggle.name == "Large Intestines") {
			description.text = "The more terminal division of the vertebrate intestine that is wider and shorter than the small intestine, typically divided into cecum, colon, and rectum, and concerned especially with the resorption of water and the formation of feces";
		}
		else if (_layerToToggle.name == "Small Intestines") {
			description.text = "The narrow part of the intestine that lies between the stomach and colon, consists of duodenum, jejunum, and ileum, secretes digestive enzymes, and is the chief site of the digestion of food into small molecules which are absorbed into the body";
		}
		else if (_layerToToggle.name == "Eyes") {
			description.text = "A specialized light-sensitive sensory structure of animals that in nearly all vertebrates, most arthropods, and some mollusks is the image-forming organ of sight";
		}
	}

	/// <summary>
	/// Resets state of button to on
	/// </summary>
	public void ResetButton()
	{
		_isToggled = true;
	}

	#endregion Events

}
