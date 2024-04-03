using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace DefaultNamespace.Hub
{
    public class TrophyManager : MonoBehaviour
    {
       
        public Boolean pirateflagbool  = false; 
        public Boolean markofdagonbool = false;
        public Boolean markofdagonbool1 = false;
        public Boolean markofdagonbool2 = false;
        public Boolean turncoatbool = false; 
        
        private Transform markofdagontrophy;
        private Transform markofdagontrophy1;
        private Transform markofdagontrophy2;
        private Transform pirateflagtropy;
        private Transform turncoattrophy;


        public bool skeletonfriendbool = false; 
        public bool skeletonfriendbool1 = false; 
        public bool skeletonfriendbool2= false;

        private Transform skeletonfriendtrophy; 
        private Transform skeletonfriendtrophy1; 
        private Transform skeletonfriendtrophy2; 
        
        public bool graverobberbool = false; 
        public bool graverobberbool1 = false;


        private Transform graverobbertrophy;
        private Transform graverobbertrophy1;


        public bool redflowerbool = false;
        private Transform redflowertrophy;

        public bool ghostlyvisagebool = false;
        private Transform ghostlyvisagetrophy;

        public Boolean peglegbool = false;
        private Transform peglegtrophy;

        public Boolean parrotbool = false;
        private Transform parrottrophy; 
        
        public Boolean freedombool = false;
        private Transform freedomtrophy;
        
        public Boolean freedombool1 = false;
        private Transform freedomtrophy1;


        public bool poopdeckbool = false;
        private Transform poopdecktrophy;

        public bool bootlickerbool = false;
        private Transform bootlickertrophy;

        public bool guiltbool = false;
        private Transform guilttrophy; 
        
        public bool guiltbool1 = false;
        private Transform guilttrophy1; 
        public void Awake()
        {
            
            markofdagontrophy = transform.Find("MarkOfDagon");
            markofdagontrophy1 = transform.Find("MarkOfDagon+1");
            markofdagontrophy2 = transform.Find("MarkOfDagon+2");
            pirateflagtropy = transform.Find("PirateFlag");
            turncoattrophy = transform.Find("Turncoat");
            skeletonfriendtrophy = transform.Find("SkeletonFriend");
            skeletonfriendtrophy1 = transform.Find("SkeletonFriend+1");
            skeletonfriendtrophy2 = transform.Find("SkeletonFriend+2");
            graverobbertrophy = transform.Find("Graverobber");
            graverobbertrophy1 = transform.Find("Graverobber-1");
            redflowertrophy = transform.Find("RedFlower");
            ghostlyvisagetrophy = transform.Find("GhostlyVisage");
            peglegtrophy = transform.Find("PegLeg");
            parrottrophy = transform.Find("Parrot");
            freedomtrophy = transform.Find("Freedom");
            freedomtrophy1 = transform.Find("Freedom+1");
            poopdecktrophy = transform.Find("PoopDeck");
            bootlickertrophy = transform.Find("Bootlicker");
            guilttrophy = transform.Find("Guilt");
            guilttrophy1 = transform.Find("Guilt+1"); 
            
            GameManager.choices.CreateDependency("PirateFlag", pirateflagtropy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Turncoat", turncoattrophy.gameObject, 1, appearTrophy); 
            GameManager.choices.CreateDependency("MarkofDagon", markofdagontrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("MarkofDagon+1", markofdagontrophy1.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("MarkofDagon+2", markofdagontrophy2.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("SkeletonFriend", skeletonfriendtrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("SkeletonFriend+1", skeletonfriendtrophy1.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("SkeletonFriend+2", skeletonfriendtrophy2.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Graverobber", graverobbertrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Graverobber-1", graverobbertrophy1.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("RedFlower", redflowertrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("GhostlyVisage", ghostlyvisagetrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("PegLeg", peglegtrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Parrot", parrottrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Freedom", freedomtrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Freedom+1", freedomtrophy1.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("PoopDeck", poopdecktrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Bootlicker", bootlickertrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Guilt", guilttrophy.gameObject, 1, appearTrophy);
            GameManager.choices.CreateDependency("Guilt+1", guilttrophy1.gameObject, 1, appearTrophy);
        }

        public void Update()
        {
            
            if (pirateflagbool)
            {
                markofdagontrophy.gameObject.SetActive(true);
            }
            
            if (turncoatbool)
            {
                turncoattrophy.gameObject.SetActive(true);
            }
            
            if (markofdagonbool)
            {
                pirateflagtropy.gameObject.SetActive(true);
            }

            if (markofdagonbool1)
            {
                markofdagontrophy1.gameObject.SetActive(true);
            }

            if (markofdagonbool2)
            {
                markofdagontrophy2.gameObject.SetActive(true);
            }

            if (skeletonfriendbool)
            {
                skeletonfriendtrophy.gameObject.SetActive(true);
            }
            if (skeletonfriendbool1)
            {
                skeletonfriendtrophy1.gameObject.SetActive(true);
            }
            if (skeletonfriendbool2)
            {
                skeletonfriendtrophy2.gameObject.SetActive(true);
            }
            if (graverobberbool)
            {
                graverobbertrophy.gameObject.SetActive(true);
            }

            if (graverobberbool1)
            {
                graverobbertrophy1.gameObject.SetActive(true);
            }

            if (redflowerbool)
            {
                redflowertrophy.gameObject.SetActive(true);
            }

            if (ghostlyvisagebool)
            {
                ghostlyvisagetrophy.gameObject.SetActive(true);
            }

            if (peglegbool)
            {
                peglegtrophy.gameObject.SetActive(true);
            }

            if (parrotbool)
            {
                parrottrophy.gameObject.SetActive(true);
            }

            if (freedombool)
            {
                freedomtrophy.gameObject.SetActive(true);
            }

            if (freedombool1)
            {
                freedomtrophy1.gameObject.SetActive(true);
            }

            if (poopdeckbool)
            {
                poopdecktrophy.gameObject.SetActive(true);
            }

            if (bootlickerbool)
            {
                bootlickertrophy.gameObject.SetActive(true);
            }

            if (guiltbool)
            {
                guilttrophy.gameObject.SetActive(true);
            }

            if (guiltbool1)
            {
                guilttrophy1.gameObject.SetActive(true);
            }
            
            
        }
        public int appearTrophy(GameObject trophy)
        {   
            Debug.Log("appear trophy" + trophy);
            trophy.SetActive(true);
            return 0; 
        }
    }
    
}