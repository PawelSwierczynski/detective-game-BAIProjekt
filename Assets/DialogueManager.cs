using Assets.Dialogues;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueTextBox;
    public Image dialogueBoxShadow;
    public Text endgameTextBox;
    public Image endgameBoxShadow;
    public SC_CharacterController characterController;
    public SC_InventorySystem inventorySystem;

    private Dialogue currentDialogue;
    private DialogueBoxState dialogueBoxState;
    private IDictionary<int, Dialogue> dialogues;

    private enum DialogueBoxState
    {
        Shown,
        Hidden
    }

    void Start()
    {
        HideDialogueBox();
        endgameBoxShadow.gameObject.SetActive(false);
        endgameBoxShadow.gameObject.SetActive(false);
        currentDialogue = null;
        dialogueBoxState = DialogueBoxState.Hidden;
        dialogues = new Dictionary<int, Dialogue>
        {
            [1] = new Dialogue("Rimald", new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("Zostaw mnie w spokoju! Nie mam ci nic do powiedzenia!", new List<Response>()
                {
                    new Response("Szukam zabójcy Twojej żony. Czy mółgbyś mi powiedzieć coś więcej na ten temat.", 1),
                }),
                [1] = new DialoguePart("Nawet nie wiesz, co musiałem przejść tamtej nocy! Moja żona leżała cała we krwi, z zakrwawionym sztyletem w ręce. Jak byś ty się czuł w moim położeniu!? Niech ja tylko dorwę tego drania, który to zrobił!", new List<Response>())
            }),
            [2] = new Dialogue("Duchowny Olivier", new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("Witaj, jestem duchownym i przewodzę tej gromadce dusz. Ostatnio, jak pewnie wiesz, zamordowano tu Romuellę. Czy dowiedziałeś się kto stoi za tą zbrodnią?", new List<Response>()
                {
                    new Response("Powiedz mi coś więcej o tej zbrodni.", 1),
                    new Response("Co robiłeś tej nocy, kiedy Romuella umarła?", 2),
                    new Response("Wiem, kto zabił Romuellę.", 3)
                }),
                [1] = new DialoguePart("Dowiedziałem się o śmierci Romuelli od jej męża, Rimalda. Przybiegł do mnie przedwczoraj w nocy i opowiedział o tym, jak znalazł ją martwą w chatce leśnej, gdzie przygotowywała medykamenty dla potrzebujących.", new List<Response>()
                {
                    new Response("A więc była zielarką?", 4),
                    new Response("Co robiłeś tej nocy, kiedy Romuella umarła?", 2)
                }),
                [2] = new DialoguePart("Spałem, aż do trzeciej warty, gdy przybiegł do mnie Rimald i opowiedział o wszystkim.", new List<Response>()),
                [3] = new DialoguePart("Któż dokonał tej jakże straszliwej zbrodni?", new List<Response>() 
                {
                    new Response("Był to Gregor.", -1),
                    new Response("Rimald zabił swoją żonę.", -1),
                    new Response("Jarom doprowadził do śmierci własną matkę.", -1),
                    new Response("Cathie ją zabiła.", 0),
                    new Response("Ty ją zabiłeś!", -1),                    
                    new Response("Popełniła samobójstwo.", -1)
                }),
                [4] = new DialoguePart("Dokładnie tak, przejęła tę rolę po poprzedniej zielarce, która okazała się być czarownicą, a, jak wiadomo, w naszym królestwie uprawianie wszelkiej formy magii jest surowo wzbronione. Należy walczyć z objawami wszelkiej herezji.", new List<Response>())
            }),
            [3] = new Dialogue("Gregor", new Dictionary<int, DialoguePart> 
            {
                [0] = new DialoguePart("Szukasz mordercy Romuelli, prawda? To niewyobrażalne, żeby ktoś mógł dokonać czegoś tak strasznego!", new List<Response>() 
                {
                    new Response("Co robiłeś tamtej nocy?", 1),
                }),
                [1] = new DialoguePart("Od czego zacząć? Romuella poprosiła mnie, abym narąbał Jej trochę drewna na opał. Wziąłem więc siekierę i poszedłem ściąć drzewo w pobliżu Jej chaty. Ścinałem właśnie stary dąb, gdy usłyszałem krzyk. Pobiegłem niezwłocznie i zobaczyłem krwawiącą Romuellę. Położyłem Ją na brzuchu i próbowałem zatamować krwawienie, ale rana była zbyt głęboka. Na zawsze będę miał ten widok przed oczami.", new List<Response>() 
                {
                    new Response("Możesz mi powiedzieć coś więcej o tej sprawie?", 2),
                    new Response("Gdzie zostawiłeś tę siekierę?", 3)
                }),
                [2] = new DialoguePart("Kiedy tylko wbiegłem do chaty, zobaczyłem stojącego nad Nią męża. Stał jak słup soli i patrzył na Nią. Myślę, że miał Jej za złe, że spędza za dużo czasu na szukaniu ziół i że zaniedbuje rodzinę.", new List<Response>()
                {
                    new Response("Gdzie zostawiłeś tę siekierę?", 3)
                }),
                [3] = new DialoguePart("Siekierę? Zostawiłem ją przy kominku. Zawsze tam ją odkładam, koło drewna na opał.", new List<Response>())
            }),
            [4] = new Dialogue("Jarom", new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("Jesteś tym Panem, który ma znaleźć osobę, która zrobiła coś złego mamie?", new List<Response>()
                {
                    new Response("Tak, jestem. Jak ostatnio zachowywała się Twoja mama?", 1)
                }),
                [1] = new DialoguePart("Ostatnio była jakaś taka smutna. Mówiła mi, że świat jest niesprawiedliwy. Tata był jakiś zły, kiedy późno wracała do domu. Mówił, że Go oszukuje, ale ja nie wiem, co to znaczy.", new List<Response>() 
                {
                    new Response("Zaczęła później wracać?", 2)
                }),
                [2] = new DialoguePart("Tak. Kilka razy wracała późno do domu. Mówiła mi, że musiała pomagać innym. Kiedyś, gdy wróciła, powiedziała, że musiała zrobić jakiś balsmam, czy jakoś tak, dla Pana Gregora, bo się zranił siekierą podczas pracy.", new List<Response>()
                {
                    new Response("Co ostatnio robiłeś?", 3)
                }),
                [3] = new DialoguePart("Wczoraj z tatą przenosiliśmy rzeczy z jej chatki. Ja... wziąłem sobie jedną książkę z Jej kufra. Miała takie duże obrazki...", new List<Response>()
                {
                    new Response("Przepraszam, czy mógłbyś mi powiedzieć, gdzie zostawiłeś tę książkę?", 4)
                }),
                [4] = new DialoguePart("Tak, proszę Pana. Zostawiłem ją na górze, tam jest więcej takich książek, ale tylko ta miała obrazki. Trochę straszne były. Ale nie mów tego tacie. Boję się, że znowu złoi mi skórę, a Pani Cathie znowu będzie mówić, że źle się zachowuję.", new List<Response>()
                {
                    new Response("Dlaczego Pani Cathie tak mówi?", 5)
                }),
                [5] = new DialoguePart("Ostatnio, gdy przyszła do mamy, stałem pod drzwiami i... podsłuchiwałem. Nie rozumiałem zbytnio, co mówią, ale mama była zła, gdy Pani Cathie poprosiła ją o zrobienie eliskiru, jakoś tak mówiła. Potem mnie nakryły, a Pani Cathie mówiła, że jestem bardzo niegrzeczny.", new List<Response>())
            }),
            [5] = new Dialogue("Cathie", new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("To bardzo smutne, że Romuella umarła. Była dobrą znajomą i zawsze była chętna pomóc.", new List<Response>()
                {
                    new Response("Co robiłaś przedwczoraj?", 1),
                    new Response("Wygląda na to, że Ją dobrze znałaś. Możesz mi powiedzieć o Niej coś więcej?", 2)
                }),
                [1] = new DialoguePart("Najpierw zrobiłam obiad dla Gregora, mojego męża. Mówił mi, że musi iść narąbać drewna i wspomniał, żebym wybrała się do miasta po nową ostrzałkę. Wzięłam trochę pieniędzy i poszłam do miasta. Kiedy wróciłam, dowiedziałam się o wszystkim. To musiało być straszne!", new List<Response>()
                {
                    new Response("Dlaczego Gregor poprosił Cię o nową ostrzałkę?", 3)
                }),
                [2] = new DialoguePart("Tak, znałam ją bardzo dobrze. Czasami nawet pomagałam jej w obowiązkach domowych. Mają bardzo niewychowanego syna, który ciągle gdzieś znika. Myślę, że przebywa w złym towarzystwie.", new List<Response>()),
                [3] = new DialoguePart("Poprzednia ostrzałka już jest zniszczona, a Gregor jest drwalem i musi dbać o swoją siekierę. Mój mąż jest często zajęty, więc ja udaję się do miasta po różne rzeczy - czy to jedzenie czy ubrania. Trzeba dbać o swój dom.", new List<Response>())
            }),
            [6] = new Dialogue("Sztylet", new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("*Na podłodze zobaczyłeś leżący sztylet. Podniosłeś go, by lepiej się przyjrzeć znalezisku.*", new List<Response>()
                {
                    new Response("*Obejrzyj sztylet.*", 1)
                }),
                [1] = new DialoguePart("*Obejrzałeś dokładnie sztylet - jego powierzchnia jest dość mocno zarysowana. Przy jelcu znalazłeś czerwony-brązowy obiekt, który wygląda na skrzep krwi. To może być narzędzie zbrodni.*", new List<Response>())
            }),
            [7] = new Dialogue("Książka", new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("*Na ziemi leży jakaś zakurzona książka.*", new List<Response>()
                {
                    new Response("*Obejrzyj książkę.*", 1)
                }),
                [1] = new DialoguePart("*Książka wygląda na starą. Okładka jest już dość zniszczona, a strony pożółkły. Tekst jednak zachował się dość dobrze. Wertując kolejne kartki znajdujesz dużo tajemniczych przepisów oraz inkantacji. W pewnym momencie zauważasz, że ktoś wyrwał kilka kartek. Jedyne, co po nich pozostało, to fragment tytułu - \"Eliksir pra\".*", new List<Response>())
            }),
            [8] = new Dialogue("Siekiera", new Dictionary<int, DialoguePart>
            {
                [0] = new DialoguePart("*Obok kominka leży siekiera. Wygląda na wysłużoną.*", new List<Response>() 
                {
                    new Response("*Uważnie obejrzyj siekierę.*", 1)
                }),
                [1] = new DialoguePart("*Siekiera wygląda na nieco zużytą, jednak jej ostrze jest bardzo ostre, jakby nie było używane od jakiegoś czasu.*", new List<Response>())
            })
        };
    }

    void Update()
    {
        if (dialogueBoxState == DialogueBoxState.Shown)
        {
            if (currentDialogue.RetrieveResponses().Count == 6)
            {
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    endgameTextBox.text = "Wskazałeś zabójcę."+ "\n" + "Wygrałeś!";
                    endgameTextBox.gameObject.SetActive(true);
                    endgameBoxShadow.gameObject.SetActive(true);
                    HideDialogueBox();
                    characterController.enabled = false;
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6))
                {
                    endgameTextBox.text = "Wskazałeś niewłaściwą osobę." + "\n" + "Przegrałeś!";
                    endgameTextBox.gameObject.SetActive(true);
                    endgameBoxShadow.gameObject.SetActive(true);
                    HideDialogueBox();
                    characterController.enabled = false;
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                HideDialogueBox();
                currentDialogue.ResetProgress();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) && currentDialogue.IsResponseAvailable(0))
            {
                currentDialogue.AdvanceToNextSpeech(0);

                UpdateDialogueBox();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && currentDialogue.IsResponseAvailable(1))
            {
                currentDialogue.AdvanceToNextSpeech(1);

                UpdateDialogueBox();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && currentDialogue.IsResponseAvailable(2))
            {
                currentDialogue.AdvanceToNextSpeech(2);

                UpdateDialogueBox();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && currentDialogue.IsResponseAvailable(3))
            {
                currentDialogue.AdvanceToNextSpeech(3);

                UpdateDialogueBox();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) && currentDialogue.IsResponseAvailable(4))
            {
                currentDialogue.AdvanceToNextSpeech(4);

                UpdateDialogueBox();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) && currentDialogue.IsResponseAvailable(5))
            {
                currentDialogue.AdvanceToNextSpeech(5);

                UpdateDialogueBox();
            }
        }
    }

    private void UpdateDialogueBox()
    {
        dialogueTextBox.text = currentDialogue.SpeecherName + "\n" + currentDialogue.RetrieveCurrentSpeech() + "\n\n";

        int responseNumber = 1;

        foreach (var responseText in currentDialogue.RetrieveResponses())
        {
            dialogueTextBox.text += responseNumber.ToString() + ". " + responseText + "\n";
            responseNumber++;
        }
    }

    public void ShowDialogueBox(int dialogueIdentifier)
    {
        currentDialogue = dialogues[dialogueIdentifier];

        UpdateDialogueBox();

        dialogueTextBox.gameObject.SetActive(true);
        dialogueBoxShadow.gameObject.SetActive(true);
        characterController.enabled = false;

        dialogueBoxState = DialogueBoxState.Shown;
    }

    private void HideDialogueBox()
    {
        dialogueTextBox.gameObject.SetActive(false);
        dialogueBoxShadow.gameObject.SetActive(false);
        characterController.enabled = true;

        dialogueBoxState = DialogueBoxState.Hidden;
    }
}