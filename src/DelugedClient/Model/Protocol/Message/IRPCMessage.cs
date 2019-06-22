namespace DelugedClient.Model.Protocol.Message
{
	public interface IRPCMessage
    {
		MessageType MessageType { get; }
    }

	public enum MessageType
	{
		Response = 1,
		Error = 2,
		Event = 3
	}
}
