namespace SecretSantaService.Domain.Pairings
{
    public interface IPairingFactory
    {
        Pairing CreatePairing(int partyId, int donorId, int recipientId);
    }
}
