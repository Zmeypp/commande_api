Feature: Gestion des commandes
  En tant qu'utilisateur de l'API de gestion des commandes
  Je veux pouvoir effectuer des opérations CRUD sur les commandes

  Background:
    Given que l'API est en cours d'exécution

  Scenario: Ajouter une nouvelle commande
    Given je suis sur la page de création de commande
    When je crée une nouvelle commande avec les détails suivants:
      | Id | Nom        | Quantité | Prix  |
      | 1  | Ordinateur | 2        | 1000€ |
    Then la commande est créée avec succès
    And je peux voir la commande dans la liste des commandes

  Scenario: Obtenir une commande existante
    Given je connais l'ID d'une commande existante
    When je demande les détails de la commande avec l'ID spécifié
    Then je reçois les détails de la commande demandée

  Scenario: Mettre à jour une commande existante
    Given je connais l'ID d'une commande existante
    And j'ai les détails mis à jour pour cette commande
    When je mets à jour la commande avec les nouveaux détails
    Then la commande est mise à jour avec succès

  Scenario: Supprimer une commande existante
    Given je connais l'ID d'une commande existante
    When je supprime la commande avec l'ID spécifié
    Then la commande est supprimée avec succès
