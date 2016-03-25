using UnityEngine;
using System.Collections;
using System;


public class Test_Proc : MonoBehaviour
{
    public int stress = 0;
    public int i = 0;
    private int note_count = 0;
    public int gamme = 0;
    private int gamme_count = 0;
    //private int[] gamme_list = new int[10] { 2, 0, 1, 0, 1, 2, 0, 2, 0, 1 };
    private int[] gamme_list = new int[12] { 3, 0, 2, 0, 2, 3, 0, 3, 0, 2, 0, 1 };
    //public bool chord_level = true;
    public int chord_level = 3;
    public int fondamentale = 0;
    private int hp_count = 0;
    private int old_fondamentale = 0;
    private int previousnote = 0;
    private double v12 = 1.05946309;
    public int frame_per_chord = 320;
    public int note_per_chord = 8;
    public AudioSource Piano_lp;
    public AudioSource Piano_lp_min;
    public AudioSource Piano_lp_dim;
    public AudioSource[] Piano_hp;
    private int[] old_note_list = new int[2];
    private int[] maj = new int[12] { 0, 3, 1, 3, 0, 2, 3, 0, 3, 1, 3, 2 };
    private int[] min = new int[12] { 0, 3, 2, 0, 3, 1, 3, 0, 2, 3, 1, 3 };
    private int[] dim = new int[12] { 0, 3, 2, 0, 3, 1, 0, 3, 1, 2, 0, 1 };


    // Fonction pour lancer le son d'un accord. 

    /*private void play_note_lp(int a)
    {
        // Le test sert à décider si on doit jouer un accord mineur ou majeur.
        if (chord_level)
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
    */

    private void play_note_lp(int a)
    {
        // Le test sert à décider si on doit jouer un accord mineur ou majeur.
        if (chord_level == 3)
        {
            Piano_lp.pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
            Piano_lp.Play();
        }
        else if (chord_level == 2)
        {
            Piano_lp_min.pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
            Piano_lp_min.Play();
        }
        else
        {
            Piano_lp_dim.pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
            Piano_lp_dim.Play();
        }
    }

    // Fonction pour lancer le son d'une note de mélodie. 
    private void play_note_hp(int a)
    {
        // Les sons sont lancés en alternance sur 4 "AudioSource" pour eviter les anomalies sonores.
        hp_count = (hp_count + 1) % 4;
        Piano_hp[hp_count].pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
        Piano_hp[hp_count].Play();
    }

    // Cette fonction sert à générer une note fondamentale et jouer un accord à la main gauche.
    private void play_note_low(ref int fondamentale, ref int old_fondamentale)
    {

        // On choisit une note fondamentale pour l'accord principal
        fondamentale = UnityEngine.Random.Range(0, 11);
        // Ensuite, on compare la fondamentale crée celle générée lors de la précédente update : si c'est la même, on prend la suivante pour éviter les répétitions
        if (stress < 40)
            while (fondamentale == old_fondamentale || gamme_list[mod(fondamentale + gamme, 12)] == 0 || gamme_list[mod(fondamentale + gamme, 12)] == 1)
                fondamentale += 1;
        else
            while (fondamentale == old_fondamentale || gamme_list[mod(fondamentale + gamme, 12)] == 0 || gamme_list[mod(fondamentale + gamme, 12)] == 3)
                fondamentale += 1;
        // On regarde si l'accord est majeur, mineur ou diminué
        chord_level = (gamme_list[mod(fondamentale + gamme, 12)]);
        // On joue l'accord à la main gauche
        play_note_lp(fondamentale);
        i = 0;
        frame_per_chord = 240 - stress;
        note_per_chord = 12 - stress / 25;
        if (stress > 65)
        {
            if ((UnityEngine.Random.Range(1, 6) < 2))
            {
                frame_per_chord /= 2;
                note_per_chord /= 2;
            }
            else if ((UnityEngine.Random.Range(1, 3) == 1))
            {
                frame_per_chord = 105;
                note_per_chord = 6;
            }
        }
        old_fondamentale = fondamentale;
    }

    // La fonction change la note de mélodie en cours, puis la joue (en appelant "play_note_hp").
    private void play_note_fondamentale(int fondamentale, ref int previousnote)
    {
        int alea = UnityEngine.Random.Range(0, 6);
        switch (alea)
        {
            case 0:
            case 1:
            case 2:
                previousnote = next_note(previousnote, true, 1, chord_level);
                break;
            case 3:
            case 4:
                previousnote = next_note(previousnote, false, 1, chord_level);
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

    // La fonction change la note de mélodie en cours, puis la joue (en appelant "play_note_hp").
    private void play_note_pentatonic(int fondamentale, ref int previousnote)
    {
        int alea = UnityEngine.Random.Range(0, 10);
        switch (alea)
        {
            case 0:
            case 1:
                previousnote = next_note(previousnote, true, 2, chord_level);
                break;
            case 2:
            case 3:
                previousnote = next_note(previousnote, true, 1, chord_level);
                break;
            case 4:
            case 5:
            case 6:
                previousnote = next_note(previousnote, false, 2, chord_level);
                break;
            case 7:
            case 8:
                previousnote = next_note(previousnote, false, 1, chord_level);
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

    // La fonction change la note de mélodie en cours, puis la joue (en appelant "play_note_hp").
    private void play_note_highpitch(int fondamentale, ref int previousnote)
    {
        int alea = UnityEngine.Random.Range(0, 10);
        switch (alea)
        {
            case 0:
            case 1:
                previousnote = next_note(previousnote, true, 3, chord_level);
                break;
            case 2:
            case 3:
                previousnote = next_note(previousnote, true, 2, chord_level);
                break;
            case 4:
            case 5:
            case 6:
                previousnote = next_note(previousnote, false, 3, chord_level);
                break;
            case 7:
            case 8:
                previousnote = next_note(previousnote, false, 2, chord_level);
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

    // Fonction pour passer à la note suivante ou précédente en evitant les dissonances ou les sortie de gamme.
    private int next_note(int n, bool rising, int niveau, int chord_level)
    {
        int pas = -1;
        if (rising)
            pas = 1;
        if (chord_level == 3)
        {
            while (true)
            {
                n = mod(n + pas, 12);
                if (maj[mod(n - fondamentale, 12)] < niveau)
                    return n;
            }
        }
        else if (chord_level == 2)
        {
            while (true)
            {
                n = mod(n + pas, 12);
                if (min[mod(n - fondamentale, 12)] < niveau)
                    return n;
            }
        }
        else
        {
            while (true)
            {
                n = mod(n + pas, 12);
                if (dim[mod(n - fondamentale, 12)] < niveau)
                    return n;
            }
        }
    }

    // Fonction de modulo, pour éviter les négatifs.
    int mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    // Use this for initialization
    void Start()
    {
        Piano_lp.volume = 0.5f + (float)stress / 200f;
        Piano_lp_min.volume = 0.5f + (float)stress / 200f;
        Piano_lp_dim.volume = 0.5f + (float)stress / 200f;
        Piano_hp[0].volume = 0.5f + (float)stress / 200f;
        Piano_hp[1].volume = 0.5f + (float)stress / 200f;
        Piano_hp[2].volume = 0.5f + (float)stress / 200f;
        Piano_hp[3].volume = 0.5f + (float)stress / 200f;
        play_note_low(ref fondamentale, ref old_fondamentale);
        int j = 2;
        int previousnote = fondamentale;
    }
    
    void Update()
    {
        i++;
        if (i % (frame_per_chord / note_per_chord) == 0)
        {
            note_count++;
            if (note_count % (note_per_chord / 4) == 0)
            {
                if (note_count % (note_per_chord / 2) == 0)
                {
                    if (note_count % (note_per_chord) == 0)
                    {
                        if (Input.GetKey("j"))
                            stress = 0;
                        else if (Input.GetKey("k"))
                            stress = 50;
                        else if (Input.GetKey("l"))
                            stress = 100;
                        play_note_low(ref fondamentale, ref old_fondamentale);
                        note_count = 0;
                        gamme_count += stress;
                        if (gamme_count > 100)
                        {
                            gamme = mod(gamme + UnityEngine.Random.Range(-2, 2), 12);
                            gamme_count = 0;
                        }
                    }
                    else
                        play_note_fondamentale(fondamentale, ref previousnote);
                    if (stress > 80)
                    {
                        fondamentale = mod(fondamentale + UnityEngine.Random.Range(-1, 1), 12);
                        if (UnityEngine.Random.Range(0, 2) < 1)
                            play_note_lp(fondamentale);
                    }
                }
                else
                {
                    play_note_pentatonic(fondamentale, ref previousnote);
                }
            }
            else
            {
                if (stress < 70)
                    play_note_highpitch(fondamentale, ref previousnote);
                else
                    play_note_pentatonic(fondamentale, ref previousnote);
            }
        }
    }
}