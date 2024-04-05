using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
	public TMP_Text nameText;
	public TMP_Text dialogueText;
    public Image dialogueImage;

	public Animator animator;
	private Queue<string> sentences;
    private string currentSentence;
    public float typingSpeed;
    public bool isTyping;

	public GameObject player;
	public PlayerStateMachine playerStateMachine;

	void Start() {
		sentences = new Queue<string>();
		player = GameObject.Find("Player");
		playerStateMachine = player.GetComponent<PlayerStateMachine>();
	}

	public void StartDialogue(Dialogue dialogue, Sprite dialogueSprite)
	{
		playerStateMachine.ChangeState(playerStateMachine.interactState);
		animator.SetBool("DialogueBoxIsOpen", true);

		nameText.text = dialogue.name;
        dialogueImage.sprite = dialogueSprite;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0 && !isTyping)
		{
			EndDialogue();
			return;
		}

        StopAllCoroutines();
        
        if(isTyping)
        {
            FinishTypingEarly(currentSentence);
        }
        else if(!isTyping)
        {
            currentSentence = sentences.Dequeue();
		    StartCoroutine(TypeSentence(currentSentence));
        }
        //when Dequeueing, get rid off the first of queue
	}

	IEnumerator TypeSentence(string sentence)
	{
        isTyping = true;

        int maxVisibleChars = 0;

		dialogueText.text = sentence;
        dialogueText.maxVisibleCharacters = maxVisibleChars;

		foreach(char letter in sentence.ToCharArray())
		{
            maxVisibleChars++;
            dialogueText.maxVisibleCharacters = maxVisibleChars;

            yield return new WaitForSecondsRealtime(typingSpeed);
		}

        isTyping = false;
	}

    private void FinishTypingEarly(string sentence)
    {
        dialogueText.text = sentence;
        dialogueText.maxVisibleCharacters = sentence.Length;

        isTyping = false;
    }

	void EndDialogue()
	{
		animator.SetBool("DialogueBoxIsOpen", false);
		playerStateMachine.interactState.ExitState();
	}
}