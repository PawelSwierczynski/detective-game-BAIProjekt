namespace Assets.Dialogues
{
    public class Response
    {
        public string Text;
        public int NextSpeechIdentifier;

        public Response(string text, int nextSpeechIdentifier)
        {
            Text = text;
            NextSpeechIdentifier = nextSpeechIdentifier;
        }
    }
}