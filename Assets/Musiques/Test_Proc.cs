using UnityEngine;
using System.Collections;
using System;


public class Test_Proc : MonoBehaviour
{
    public string starting_seed;
    private int multiplier = 302834;
    private int seed = 1;
    private int increment = 1;
    private int modulo = 43261;
    public int stress = 0;
    public int i = 0;
    private int note_count = 0;
    public int gamme = 0;
    private int gamme_count = 0;
    private int current_gamme = 0;
    //private int[] gamme_list = new int[10] { 2, 0, 1, 0, 1, 2, 0, 2, 0, 1 };
    public enum Gamme : byte {Nope, Maj, Min, Dim, Aug };
    private Gamme[] gamme_list_maj = new Gamme[12] { Gamme.Maj, Gamme.Nope, Gamme.Min, Gamme.Nope, Gamme.Min, Gamme.Maj, Gamme.Nope, Gamme.Maj, Gamme.Nope, Gamme.Min, Gamme.Nope, Gamme.Dim };
    private Gamme[] gamme_list_min = new Gamme[12] { Gamme.Min, Gamme.Nope, Gamme.Dim, Gamme.Maj, Gamme.Nope, Gamme.Min, Gamme.Nope, Gamme.Min, Gamme.Maj, Gamme.Nope, Gamme.Maj, Gamme.Nope };
    private Gamme[] gamme_list_dim = new Gamme[12] { Gamme.Dim, Gamme.Nope, Gamme.Dim, Gamme.Dim, Gamme.Nope, Gamme.Min, Gamme.Dim, Gamme.Nope, Gamme.Min, Gamme.Dim, Gamme.Nope, Gamme.Dim };
    private Gamme[] gamme_list_aug = new Gamme[12] { Gamme.Aug, Gamme.Nope, Gamme.Nope, Gamme.Aug, Gamme.Min, Gamme.Nope, Gamme.Nope, Gamme.Aug, Gamme.Min, Gamme.Nope, Gamme.Nope, Gamme.Aug };
    private Gamme[] gamme_list_uni = new Gamme[12] { Gamme.Min, Gamme.Min, Gamme.Aug, Gamme.Dim, Gamme.Min, Gamme.Min, Gamme.Aug, Gamme.Dim, Gamme.Min, Gamme.Min, Gamme.Aug, Gamme.Dim };
    private Gamme[][] gamme_list_list = new Gamme[5][];
    //private int[][] gamme_list_list = new int[5][] { gamme_list_maj, gamme_list_min, gamme_list_dim, gamme_list_aug, gamme_list_uni };
    //public bool chord_level = true;
    public Gamme chord_level = Gamme.Maj;
    public int fondamentale = 0;
    private int hp_count = 0;
    private int old_fondamentale = 0;
    private int previousnote;
    private double v12 = 1.05946309;
    private int frame_per_chord = 320;
    private int note_per_chord = 8;
    public AudioSource Piano_lp;
    public AudioSource Piano_lp_min;
    public AudioSource Piano_lp_dim;
    public AudioSource Piano_lp_aug;
    public AudioSource[] Piano_hp;
    private int[] old_note_list = new int[2];
    private int[] maj = new int[24] { 0, 3, 1, 3, 0, 2, 3, 0, 3, 1, 3, 2, 0, 3, 1, 3, 0, 2, 3, 0, 3, 1, 3, 2 };
    private int[] min = new int[24] { 0, 3, 2, 0, 3, 1, 3, 0, 2, 3, 1, 3, 0, 3, 2, 0, 3, 1, 3, 0, 2, 3, 1, 3 };
    private int[] dim = new int[24] { 0, 3, 2, 0, 3, 1, 0, 3, 1, 2, 3, 1, 0, 3, 2, 0, 3, 1, 0, 3, 1, 2, 3, 1 };
    private int[] aug = new int[24] { 0, 3, 3, 1, 0, 3, 3, 1, 0, 3, 3, 2, 0, 3, 3, 1, 0, 3, 3, 1, 0, 3, 3, 2 };
    private int[] uni = new int[24] { 0, 3, 1, 3, 0, 3, 1, 3, 0, 3, 2, 3, 0, 3, 1, 3, 0, 3, 1, 3, 0, 3, 2, 3 };
    public short[] prob_array = new short[5] { 25, 20, 15, 15, 10 };
    //public GameObject tiles;
    //public GameObject tiles_low;
    //public GameObject rythme;


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
        for (int i = 0; i < 27; i++)
        {
            //tiles_low.transform.GetChild(i).GetComponent<Renderer>().material.color = return_color_2(i);
        }
        // Le test sert à décider si on doit jouer un accord mineur ou majeur.
        if (chord_level == Gamme.Maj)
        {
            Piano_lp.pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
            Piano_lp.Play();
        }
        else if (chord_level == Gamme.Min)
        {
            Piano_lp_min.pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
            Piano_lp_min.Play();
        }
        else if (chord_level == Gamme.Dim)
        {
            Piano_lp_dim.pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
            Piano_lp_dim.Play();
        }
        else
        {
            Piano_lp_aug.pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
            Piano_lp_aug.Play();
        }
    }

    // Fonction pour lancer le son d'une note de mélodie. 
    private void play_note_hp(int a)
    {
        // Les sons sont lancés en alternance sur 4 "AudioSource" pour eviter les anomalies sonores.
        hp_count = (hp_count + 1) % 4;
        Piano_hp[hp_count].pitch = (float)(Math.Pow(v12, a)) * (1 - (float)stress / 500);
        Piano_hp[hp_count].Play();
        //tiles.transform.GetChild(previousnote).GetComponent<Renderer>().material.color = return_color(a);
    }

    // Cette fonction sert à générer une note fondamentale et jouer un accord à la main gauche.
    private void play_note_low(ref int fondamentale, ref int old_fondamentale)
    {
        // On choisit une note fondamentale pour l'accord principal
        fondamentale = seed % 12;
        // Ensuite, on compare la fondamentale crée celle générée lors de la précédente update : si c'est la même, on prend la suivante pour éviter les répétitions
        if (stress < 36)
            while (fondamentale == old_fondamentale || gamme_list_list[current_gamme][mod(fondamentale + gamme, 12)] == Gamme.Nope || gamme_list_list[current_gamme][mod(fondamentale + gamme, 12)] == Gamme.Dim)
                fondamentale = mod(fondamentale+1, 12);
        else if (stress > 64)
            while (fondamentale == old_fondamentale || gamme_list_list[current_gamme][mod(fondamentale + gamme, 12)] == Gamme.Nope)
                fondamentale = mod(fondamentale + 1, 12);
        else
            while (fondamentale == old_fondamentale || gamme_list_list[current_gamme][mod(fondamentale + gamme, 12)] == Gamme.Nope)
                fondamentale = mod(fondamentale + 1, 12);
        // On regarde si l'accord est majeur, mineur ou diminué
        chord_level = (gamme_list_list[current_gamme][mod(fondamentale + gamme, 12)]);
        // On joue l'accord à la main gauche
        play_note_lp(fondamentale);
        i = 0;
        frame_per_chord = 150 - stress/2;
        note_per_chord = 8 + stress / 33;
        if (stress > 80)
        {
            if (seed < 7210)
            {
                frame_per_chord /= 2;
                note_per_chord /= 2;
            }
            else if (seed > 28840)
            {
                frame_per_chord = 75;
                note_per_chord = 6;
            }
        }
        old_fondamentale = fondamentale;
    }

    // La fonction change la note de mélodie en cours, puis la joue (en appelant "play_note_hp").
    private void play_note_fondamentale(int fondamentale, ref int previousnote)
    {
        //tiles.transform.GetChild(old_note_list[0]).GetComponent<Renderer>().material.color = Color.white;
        //tiles.transform.GetChild(old_note_list[1]).GetComponent<Renderer>().material.color = Color.white;
        int alea = seed / 433;
        short x = prob_array[0];
        if (alea < x)
        {
            // Cas "Note Suivante"
            previousnote = next_note(previousnote, true, 1);
            prob_array[0] -= 4;
            prob_array[1] += 1;
            prob_array[2] += 1;
            prob_array[3] += 1;
            prob_array[4] += 1;
        }
        else
        {
            x += prob_array[1];
            if (alea < x)
            {
                // Cas "Note précédente"
                previousnote = next_note(previousnote, false, 1);
                prob_array[0] += 1;
                prob_array[1] -= 4;
                prob_array[2] += 1;
                prob_array[3] += 1;
                prob_array[4] += 1;
            }
            else
            {
                x += prob_array[2];
                if (alea < x)
                {
                    // Cas "Note Fondamentale"
                    previousnote = fondamentale;
                    prob_array[0] += 1;
                    prob_array[1] += 1;
                    prob_array[2] -= 4;
                    prob_array[3] += 1;
                    prob_array[4] += 1;
                }
                else
                {
                    x += prob_array[3];
                    if (alea < x)
                    {
                        // Cas "Note Quinte"
                        previousnote = next_note(previousnote, true, 1);
                        prob_array[0] += 1;
                        prob_array[1] += 1;
                        prob_array[2] += 1;
                        prob_array[3] -= 4;
                        prob_array[4] += 1;
                    }
                    else
                    {
                        x += prob_array[4];
                        if (alea < x)
                        {
                            // Cas "Note Tierce"
                            previousnote = next_note(previousnote, false, 1);
                            prob_array[0] += 1;
                            prob_array[1] += 1;
                            prob_array[2] += 1;
                            prob_array[3] += 1;
                            prob_array[4] -= 4;
                        }
                        else
                            // Cas "Silence"
                            return;
                    }
                }
            }
        }
        if (previousnote == old_note_list[0] || previousnote == old_note_list[1])
            return;
        play_note_hp(previousnote);
        if (stress < 41 && previousnote > 4 && (seed % 7 == 0 || seed % 5 == 0))
        {
            int a = next_note(previousnote, false, 1);
            play_note_hp(a);
            old_note_list[0] = a;
            old_note_list[1] = previousnote;
        }
        else
        {
            old_note_list[0] = old_note_list[1];
            old_note_list[1] = previousnote;
        }
    }
    /*private void play_note_fondamentale(int fondamentale, ref int previousnote)
    {
        int alea =  e.Range(0, 7);
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
    } */

    // La fonction change la note de mélodie en cours, puis la joue (en appelant "play_note_hp").
    private void play_note_pentatonic(int fondamentale, ref int previousnote)
    {
        //tiles.transform.GetChild(old_note_list[0]).GetComponent<Renderer>().material.color = Color.white;
        //tiles.transform.GetChild(old_note_list[1]).GetComponent<Renderer>().material.color = Color.white;
        int alea = seed / 433;
        short x = prob_array[0];
        if (alea < x)
        {
            // Cas "Note Suivante"
            previousnote = next_note(previousnote, true, 2);
            prob_array[0] -= 4;
            prob_array[1] += 1;
            prob_array[2] += 1;
            prob_array[3] += 1;
            prob_array[4] += 1;
        }
        else
        {
            x += prob_array[1];
            if (alea < x)
            {
                // Cas "Note précédente"
                previousnote = next_note(previousnote, false, 2);
                prob_array[0] += 1;
                prob_array[1] -= 4;
                prob_array[2] += 1;
                prob_array[3] += 1;
                prob_array[4] += 1;
            }
            else
            {
                x += prob_array[2];
                if (alea < x)
                {
                    // Cas "Note Fondamentale"
                    previousnote = next_note(previousnote, true, 1);
                    prob_array[0] += 1;
                    prob_array[1] += 1;
                    prob_array[2] -= 4;
                    prob_array[3] += 1;
                    prob_array[4] += 1;
                }
                else
                {
                    x += prob_array[3];
                    if (alea < x)
                    {
                        // Cas "Note Quinte"
                        previousnote = next_note(previousnote, false, 1);
                        prob_array[0] += 1;
                        prob_array[1] += 1;
                        prob_array[2] += 1;
                        prob_array[3] -= 4;
                        prob_array[4] += 1;
                    }
                    else
                    {
                        x += prob_array[4];
                        if (alea < x)
                        {
                            // Cas "Note Tierce"
                            previousnote = fondamentale;
                            if (stress > 74)
                                previousnote += 12;
                            prob_array[0] += 1;
                            prob_array[1] += 1;
                            prob_array[2] += 1;
                            prob_array[3] += 1;
                            prob_array[4] -= 4;
                        }
                        else
                            // Cas "Silence"
                            return;
                    }
                }
            }
        }
        if (previousnote == old_note_list[0] || previousnote == old_note_list[1])
            return;
        play_note_hp(previousnote);
        if (stress < 21 && previousnote > 4 && (seed % 7 == 0 || seed % 5 == 0))
        {
            int a = next_note(next_note(previousnote, false, 2), false, 1);
            play_note_hp(a);
            old_note_list[0] = a;
            old_note_list[1] = previousnote;
        }
        else
        {
            old_note_list[0] = old_note_list[1];
            old_note_list[1] = previousnote;
        }
    }
    /*private void play_note_pentatonic(int fondamentale, ref int previousnote)
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
    }*/

    // La fonction change la note de mélodie en cours, puis la joue (en appelant "play_note_hp").
    private void play_note_highpitch(int fondamentale, ref int previousnote)
    {
        //tiles.transform.GetChild(old_note_list[0]).GetComponent<Renderer>().material.color = Color.white;
        //tiles.transform.GetChild(old_note_list[1]).GetComponent<Renderer>().material.color = Color.white;
        int alea = seed / 433;
        short x = prob_array[0];
        if (alea < x)
        {
            // Cas "Note Suivante"
            previousnote = next_note(previousnote, true, 3);
            prob_array[0] -= 4;
            prob_array[1] += 1;
            prob_array[2] += 1;
            prob_array[3] += 1;
            prob_array[4] += 1;
        }
        else
        {
            x += prob_array[1];
            if (alea < x)
            {
                // Cas "Note précédente"
                previousnote = next_note(previousnote, false, 3);
                prob_array[0] += 1;
                prob_array[1] -= 4;
                prob_array[2] += 1;
                prob_array[3] += 1;
                prob_array[4] += 1;
            }
            else
            {
                x += prob_array[2];
                if (alea < x)
                {
                    // Cas "Note Fondamentale"
                    previousnote = next_note(previousnote, true, 2);
                    prob_array[0] += 1;
                    prob_array[1] += 1;
                    prob_array[2] -= 4;
                    prob_array[3] += 1;
                    prob_array[4] += 1;
                }
                else
                {
                    x += prob_array[3];
                    if (alea < x)
                    {
                        // Cas "Note Quinte"
                        previousnote = next_note(previousnote, false, 2);
                        prob_array[0] += 1;
                        prob_array[1] += 1;
                        prob_array[2] += 1;
                        prob_array[3] -= 4;
                        prob_array[4] += 1;
                    }
                    else
                    {
                        x += prob_array[4];
                        if (alea < x)
                        {
                            // Cas "Note Tierce"
                            previousnote = fondamentale;
                            prob_array[0] += 1;
                            prob_array[1] += 1;
                            prob_array[2] += 1;
                            prob_array[3] += 1;
                            prob_array[4] -= 4;
                        }
                        else
                            // Cas "Silence"
                            return;
                    }
                }
            }
        }
        if (previousnote == old_note_list[0] || previousnote == old_note_list[1])
            return;
        play_note_hp(previousnote);
        old_note_list[0] = old_note_list[1];
        old_note_list[1] = previousnote;
    }
    /*private void play_note_highpitch(int fondamentale, ref int previousnote)
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
    }*/

    // Fonction pour passer à la note suivante ou précédente en evitant les dissonances ou les sortie de gamme.
    private int next_note(int n, bool rising, int niveau)
    {
        int pas = -1;
        if (rising)
            pas = 1;
        n = mod(n + pas, 24);
        if (chord_level == Gamme.Maj)
        {
            while (maj[mod(n - fondamentale, 24)] >= niveau)
                n = mod(n + pas, 24);
            return n;
        }
        else if (chord_level == Gamme.Min)
        {
            while (min[mod(n - fondamentale, 24)] >= niveau)
                n = mod(n + pas, 24);
            return n;
        }
        else if (chord_level == Gamme.Dim)
        {
            while (dim[mod(n - fondamentale, 24)] >= niveau)
                n = mod(n + pas, 24);
            return n;
        }
        else if (chord_level == Gamme.Aug)
        {
            while (aug[mod(n - fondamentale, 24)] >= niveau)
                n = mod(n + pas, 24);
            return n;
        }
        else
        {
            while (uni[mod(n - fondamentale, 24)] >= niveau)
                n = mod(n + pas, 24);
            return n;
        }
    }

    // Fonction de modulo, pour éviter les négatifs.
    int mod(int x, int m)
    {
        int r = x % m;
        return r < 0 ? r + m : r;
    }

    // Fonction test de couleur
    /*Color return_color(int a)
    {
        if (chord_level == Gamme.Maj && maj[mod(a - fondamentale, 24)] == 0 || chord_level == Gamme.Min && min[mod(a - fondamentale, 24)] == 0 || chord_level == Gamme.Dim && dim[mod(a - fondamentale, 24)] == 0 || chord_level == Gamme.Aug && aug[mod(a - fondamentale, 24)] == 0)
            return Color.green;
        else if (chord_level == Gamme.Maj && maj[mod(a - fondamentale, 24)] == 1 || chord_level == Gamme.Min && min[mod(a - fondamentale, 24)] == 1 || chord_level == Gamme.Dim && dim[mod(a - fondamentale, 24)] == 1 || chord_level == Gamme.Aug && aug[mod(a - fondamentale, 24)] == 1)
            return Color.blue;
        else if (chord_level == Gamme.Maj && maj[mod(a - fondamentale, 24)] == 2 || chord_level == Gamme.Min && min[mod(a - fondamentale, 24)] == 2 || chord_level == Gamme.Dim && dim[mod(a - fondamentale, 24)] == 2 || chord_level == Gamme.Aug && aug[mod(a - fondamentale, 24)] == 2)
            return Color.red;
        else
            return Color.black;
    }
    Color return_color_2(int a)
    {
        if (chord_level == Gamme.Maj)
        {
            if (maj[mod(a - fondamentale, 24)] == 0)
                return new Color32(0, 180, 0, 255);
            else if (maj[mod(a - fondamentale, 24)] == 1)
                return new Color32(100, 255, 100, 255);
            else if (maj[mod(a - fondamentale, 24)] == 2)
                return new Color32(200, 255, 200, 255);
            else
                return Color.black;
        }
        else if (chord_level == Gamme.Min)
        {
            if (min[mod(a - fondamentale, 24)] == 0)
                return new Color32(0, 0, 180, 255);
            else if (min[mod(a - fondamentale, 24)] == 1)
                return new Color32(100, 100, 255, 255);
            else if (min[mod(a - fondamentale, 24)] == 2)
                return new Color32(200, 200, 255, 255);
            else
                return Color.black;
        }
        else if (chord_level == Gamme.Dim)
        {
            if (dim[mod(a - fondamentale, 24)] == 0)
                return new Color32(180, 0, 0, 255);
            else if (dim[mod(a - fondamentale, 24)] == 1)
                return new Color32(255, 100, 100, 255);
            else if (dim[mod(a - fondamentale, 24)] == 2)
                return new Color32(255, 200, 200, 255);
            else
                return Color.black;
        }
        else
        {
            if (aug[mod(a - fondamentale, 24)] == 0)
                return new Color32(180, 150, 0, 255);
            else if (aug[mod(a - fondamentale, 24)] == 1)
                return new Color32(255, 180, 100, 255);
            else if (aug[mod(a - fondamentale, 24)] == 2)
                return new Color32(255, 255, 200, 255);
            else
                return Color.black;
        }
    }
    */
// Use this for initialization
    void Start()
    {
        starting_seed = GameObject.Find("Settings").GetComponent<Settings>().musicSeed;
        for (int i = 0; i < starting_seed.Length; i++)
        {
            if (i % 2 == 0)
                seed = 13 * seed + Convert.ToInt32(starting_seed[i]);
            else
                increment = 17 * increment + Convert.ToInt32(starting_seed[i]);
        }
        seed = mod(seed, modulo);
        increment = mod(increment, modulo);
        int previousnote = 0;
        gamme_list_list[0] = gamme_list_maj;
        gamme_list_list[1] = gamme_list_min;
        gamme_list_list[2] = gamme_list_dim;
        gamme_list_list[3] = gamme_list_aug;
        gamme_list_list[4] = gamme_list_uni;
    }
    
    void FixedUpdate()
    {
        i++;
        if (i > (frame_per_chord / note_per_chord))
        {
            i = 0;
            seed = mod((multiplier * seed + increment), modulo);
            //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.grey;
            note_count = mod(note_count+1, note_per_chord);
            switch (note_count)
            {
                case 0:
                    if (stress < 36)
                        current_gamme = 0;
                    else if (stress < 66)
                        current_gamme = 1;
                    else
                        current_gamme = 4;
                    play_note_low(ref fondamentale, ref old_fondamentale);
                    gamme_count += stress;
                    /*
                    if (gamme_count > 100)
                    {
                        gamme = mod(gamme + UnityEngine.Random.Range(-2, 2), 12);
                        gamme_count = 0;
                    }
                    */
                    if (stress < 85)
                    {
                        play_note_fondamentale(fondamentale, ref previousnote);
                        //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.green;
                    }
                    else
                    {
                        play_note_highpitch(fondamentale, ref previousnote);
                        //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.red;
                    }
                    break;
                case 1:
                    play_note_pentatonic(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 2:
                    play_note_highpitch(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.red;
                    break;
                case 3:
                    play_note_pentatonic(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 4:
                    if (stress < 85)
                    {
                        play_note_fondamentale(fondamentale, ref previousnote);
                        //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.green;
                    }
                    else
                    {
                        play_note_highpitch(fondamentale, ref previousnote);
                        //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.red;
                    }
                    break;
                case 5:
                    play_note_pentatonic(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 6:
                    play_note_highpitch(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.red;
                    break;
                case 7:
                    play_note_pentatonic(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 8:
                    if (stress < 85)
                    {
                        play_note_fondamentale(fondamentale, ref previousnote);
                        //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.green;
                    }
                    else
                    {
                        play_note_highpitch(fondamentale, ref previousnote);
                        //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.red;
                    }
                    break;
                case 9:
                    play_note_pentatonic(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case 10:
                    play_note_highpitch(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.red;
                    break;
                default:
                    play_note_pentatonic(fondamentale, ref previousnote);
                    //rythme.transform.GetChild(note_count).GetComponent<Renderer>().material.color = Color.blue;
                    break;
            }
        }
    }
/*
    void FixedUpdate()
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
    }*/
}
