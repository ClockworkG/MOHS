using UnityEngine;
using System.Collections;
using System;


public class Test_Proc : MonoBehaviour
{

    public int i = 0;
    public int gamme = 0;
    private int[] gamme_list = new int[10] { 2, 0, 1, 0, 1, 2, 0, 2, 0, 1 };
    public bool is_maj = true;
    public int fondamentale = 0;
    private int hp_count = 0;
    private int old_fondamentale = 0;
    private int previousnote = 0;
    private double v12 = 1.05946309;
    public int frame_per_chord = 320;
    public int note_per_chord = 8;
    public AudioSource Piano_lp;
    public AudioSource Piano_lp_min;
    public AudioSource[] Piano_hp;
    private int[] old_note_list = new int[2];
    private int[] maj = new int[12] { 0, 3, 1, 3, 0, 2, 3, 0, 3, 1, 3, 2 };
    private int[] min = new int[12] { 0, 3, 2, 0, 3, 1, 3, 0, 2, 3, 1, 3 };


    //Fonction pour lancer le son d'un accord. Le "if" sert à décider si on doit jouer un accord mineur ou majeur.
    private void play_note_lp(int a)
    {
        if (is_maj)
        {
            Piano_lp.pitch = (float)(Math.Pow(v12, a));
            Piano_lp.Play();
        }
        else
        {
            Piano_lp_min.pitch = (float)(Math.Pow(v12, a));
            Piano_lp_min.Play();
        }
    }

    /* Fonction pour lancer le son d'une note de mélodie. 
    Les sons sont lancés en alternance sur 4 "AudioSource" 
    pour eviter les anomalies sonores dûe à 
    deux notes de mélodie s'enchaînant trop rapidement.*/
    private void play_note_hp(int a)
    {
        hp_count = (hp_count + 1) % 4;
        Piano_hp[hp_count].pitch = (float)(Math.Pow(v12, a));
        Piano_hp[hp_count].Play();
    }

    //Cette fonction sert à générer une note fondamentale et jouer un accord à la main gauche.
    private void play_note_low(ref int fondamentale, ref int old_fondamentale)
    {
        // On choisit une note fondamentale pour l'accord principal
        fondamentale = UnityEngine.Random.Range(0, 9);
        // Ensuite, on compare la fondamentale crée celle générée lors de la précédente update : si c'est la même, on prend la suivante pour éviter les répétitions
        while (fondamentale == old_fondamentale || gamme_list[mod(fondamentale + gamme, 10)] == 0)
            fondamentale += 1;
        old_fondamentale = fondamentale;
        // On regarde si l'accord est mineur ou majeur
        if (gamme_list[mod(fondamentale + gamme, 10)] == 1)
            is_maj = false;
        else
            is_maj = true;
        // On joue l'accord à la main gauche
        play_note_lp(fondamentale);
        i = 0;
    }

    //Ces trois fonctions changent la note de mélodie en cours, puis la joue (en appelant "play_note_hp").
    private void play_note_fondamentale(int fondamentale, ref int previousnote)
    {
        int alea = UnityEngine.Random.Range(0, 6);
        switch (alea)
        {
            case 0:
            case 1:
            case 2:
                previousnote = next_note(previousnote, true, 1, is_maj);
                break;
            case 3:
            case 4:
                previousnote = next_note(previousnote, false, 1, is_maj);
                break;
            default:
                return;
        }
        if (previousnote == old_note_list[0] || previousnote == old_note_list[1])
            return;
        play_note_hp(previousnote);
        old_note_list[0] = old_note_list[1];
        old_note_list[1] = previousnote;
    }
    private void play_note_pentatonic(int fondamentale, ref int previousnote)
    {
        int alea = UnityEngine.Random.Range(0, 10);
        switch (alea)
        {
            case 0:
            case 1:
                previousnote = next_note(previousnote, true, 2, is_maj);
                break;
            case 2:
            case 3:
                previousnote = next_note(previousnote, true, 1, is_maj);
                break;
            case 4:
            case 5:
            case 6:
                previousnote = next_note(previousnote, false, 2, is_maj);
                break;
            case 7:
            case 8:
                previousnote = next_note(previousnote, false, 1, is_maj);
                break;
            default:
                return;
        }
        if (previousnote == old_note_list[0] || previousnote == old_note_list[1])
            return;
        play_note_hp(previousnote);
        old_note_list[0] = old_note_list[1];
        old_note_list[1] = previousnote;
    }
    private void play_note_highpitch(int fondamentale, ref int previousnote)
    {
        int alea = UnityEngine.Random.Range(0, 10);
        switch (alea)
        {
            case 0:
            case 1:
                previousnote = next_note(previousnote, true, 3, is_maj);
                break;
            case 2:
            case 3:
                previousnote = next_note(previousnote, true, 2, is_maj);
                break;
            case 4:
            case 5:
            case 6:
                previousnote = next_note(previousnote, false, 3, is_maj);
                break;
            case 7:
            case 8:
                previousnote = next_note(previousnote, false, 2, is_maj);
                break;
            default:
                return;
        }
        if (previousnote == old_note_list[0] || previousnote == old_note_list[1])
            return;
        play_note_hp(previousnote);
        old_note_list[0] = old_note_list[1];
        old_note_list[1] = previousnote;
    }

    //Fonction pour passer à la note suivante ou précédente en evitant les dissonances ou les sortie de gamme.
    private int next_note(int n, bool rising, int niveau, bool majeur)
    {
        int pas = -1;
        if (rising)
            pas = 1;
        if (majeur)
        {
            while (true)
            {
                n = mod(n + pas, 12);
                if (maj[mod(n - fondamentale, 12)] < niveau)
                    return n;
            }
        }
        else
        {
            while (true)
            {
                n = mod(n + pas, 12);
                if (min[mod(n - fondamentale, 12)] < niveau)
                    return n;
            }
        }
    }

    //Fonction de modulo, pour éviter les négatifs.
    int mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    // Use this for initialization
    void Start()
    {
        int j = 2;
        int previousnote = fondamentale;
    }

    // Update is called once per frame
    void Update()
    {
        if (mod(i, (frame_per_chord / note_per_chord)) == 0)
        {
            if (i > frame_per_chord)
            {
                play_note_low(ref fondamentale, ref old_fondamentale);
                i = 0;
            }
            // Régulièrement, on génère une nouvelle note pour la mélodie
            else if (i % (frame_per_chord / (note_per_chord / 4)) == 0)
                play_note_fondamentale(fondamentale, ref previousnote);
            else if (i % (frame_per_chord / (note_per_chord / 2)) == 0)
                play_note_pentatonic(fondamentale, ref previousnote);
            else if (i % (frame_per_chord / note_per_chord) == 0)
                play_note_highpitch(fondamentale, ref previousnote);
        }
        i++;
    }
}