namespace SpaceHouse.Domain.Enums
{
    public enum TypeModule
    {
        QuartiersDeRepos,   // SleepingQuarters → zone où les astronautes dorment
        Cuisine,            // Galley → espace pour préparer et manger les repas
        Exercice,           // Exercise → salle de sport pour l’entraînement physique
        Medical,            // Medical → infirmerie ou poste de soins
        RecyclageDesDechets,// WasteRecycling → module pour le traitement et recyclage des déchets
        Laboratoire,        // Lab → espace de recherche scientifique
        SasDePressurisation // Airlock → sas reliant l’intérieur à l’extérieur
    }
}
