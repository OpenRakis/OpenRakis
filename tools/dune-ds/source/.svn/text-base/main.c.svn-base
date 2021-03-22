
#include <PA9.h>
#include "sfxa.h"
#include "sfxb.h"
#include "unhsq.h"
#include "dunespr.h"
#include "dunesal.h"
#include "commun.h"



static char message[100];



int main(int argc, char** argv)
{
    ST_UNHSQ *data_hsq;
	ST_UNHSQ *data_hsq2;
	ST_UNHSQ *data_hsq3;

	ST_DUNESPR *spr_salle;
	ST_DUNESPR *spr_toreau;
	ST_DUNESPR *spr_sang;
	ST_DUNESPR *spr_sol;
	ST_DUNESPR *spr_porte1;
	ST_DUNESPR *spr_porte2;
	ST_DUNESPR *spr_embleme;
	ST_DUNESPR *spr_colonne1;
	ST_DUNESPR *spr_colonne0;
	ST_DUNESPR *spr_flag;
	ST_DUNESPR *spr_garde;
	ST_DUNESPR *spr_peau_garde;

	ST_DUNESPR *spr_pierre_ihm;
	ST_DUNESPR *spr_directions;
	ST_DUNESPR *spr_ihm_left;
	ST_DUNESPR *spr_ihm_right;
	ST_DUNESPR *spr_ihm_perso[9];

	ST_DUNESPR_LIST* liste_spr;
	ST_UNHSQ* sal_ptr;

	int decalage_x=0;
	int decalage_x1=0;
	int st_x1=0,st_x2=0;
	int do_aff=1;

	int i;
	int numero_salle =0;

	char *nom_spr_files[9]={"dune/POR.HSQ",  "dune/PROUGE.HSQ", "dune/BALCON.HSQ","dune/SERRE.HSQ", "dune/COMM.HSQ", "dune/CORR.HSQ",
	"dune/EQUI.HSQ", "dune/MIRROR.HSQ"};
	int numero_spr_list =0;

	PA_Init();
	PA_InitVBL();
	PA_InitText(1, 0);

    
    if(!fatInitDefault()) {
        PA_OutputText(1, 1, 1, "Fat init error !!!");
        return 1;
    }

	PA_InitASLibForMP3(AS_MODE_MP3 | AS_MODE_SURROUND | AS_MODE_16CH);	
	AS_MP3StreamPlay("dune/05_-_arrakeen_palace.mp3");
	AS_SetMP3Loop(true);

	/*data_hsq = (ST_UNHSQ *) unhsq_getdata("dune/POR.HSQ");
	if (!data_hsq){
		PA_OutputText(1, 1, 1, "unhsq_load_error !!!");
        return 1;
	}*/

	data_hsq2= (ST_UNHSQ *) unhsq_getdata("dune/ICONES.HSQ");
	if (!data_hsq2){
		PA_OutputText(1, 1, 1, "unhsq_load_error !!!");
        return 1;
	}

	data_hsq3= (ST_UNHSQ *) unhsq_getdata("dune/PERS.HSQ");
	if (!data_hsq3){
		PA_OutputText(1, 1, 1, "unhsq_load_error !!!");
        return 1;
	}

	//PA_Init8bitBg(0, 3);
	PA_Init8bitDblBuffer(0, 3);
	for (i=0;i<256;i++) PA_SetBgPalCol(0, i, PA_RGB(0, 0, 0));
	
	
	DuneSpr_LoadPalette(data_hsq3);
	DuneSpr_LoadPalette(data_hsq2);
	/*DuneSpr_LoadPalette(data_hsq);*/


	
	//PA_SetBgPalCol(0, 186, PA_RGB(0x70>>3, 0x70>>3, 0));
	//PA_SetBgPalCol(0, 187, PA_RGB(0x80>>3, 0x00>>3, 0));
	//PA_SetBgPalCol(0, 188, PA_RGB(0xC0>>3, 0xC0>>3, 0));
	//PA_SetBgPalCol(0, 189, PA_RGB(0xDA>>3, 0xDA>>3, 0));
	//PA_SetBgPalCol(0, 190, PA_RGB(0xFF>>3, 0xFF>>3, 0));
	
	// editer palette sable a la main
	/*spr_salle = DuneSpr_GetSpriteData(data_hsq, 5);
	spr_toreau = DuneSpr_GetSpriteData(data_hsq, 22);
	spr_sang = DuneSpr_GetSpriteData(data_hsq, 8);
	spr_sol = DuneSpr_GetSpriteData(data_hsq, 20);
	spr_porte1 = DuneSpr_GetSpriteData(data_hsq, 13);
	spr_porte2 = DuneSpr_GetSpriteData(data_hsq, 3);
	spr_embleme = DuneSpr_GetSpriteData(data_hsq, 18);
	spr_colonne0 = DuneSpr_GetSpriteData(data_hsq, 1);
	spr_colonne1 = DuneSpr_GetSpriteData(data_hsq, 19);
	spr_flag = DuneSpr_GetSpriteData(data_hsq, 2);
	spr_garde = DuneSpr_GetSpriteData(data_hsq, 39);
	spr_peau_garde = DuneSpr_GetSpriteData(data_hsq, 41);*/

	//spr_rambarde = DuneSpr_GetSpriteData(data_hsq, 4);
	//spr_entree = DuneSpr_GetSpriteData(data_hsq, 1);
	//spr_orni = DuneSpr_GetSpriteData(data_hsq, 3);
	//spr_sable = DuneSpr_GetSpriteData(data_hsq, 8);

	//PA_SetBgPalCol(0, 240, PA_RGB(0xFF>>3, 0xFF>>3, 0xFF>>3));
	//PA_SetBgPalCol(0, 239, PA_RGB(0, 0xFF>>3, 0xFF>>3));
	//PA_SetBgPalCol(0, 245, PA_RGB(0xFF>>3, 0, 0xFF>>3));

	spr_ihm_left = DuneSpr_GetSpriteData(data_hsq2, 0);
	spr_ihm_right = DuneSpr_GetSpriteData(data_hsq2, 3);
	spr_pierre_ihm = DuneSpr_GetSpriteData(data_hsq2, 12);
	spr_directions = DuneSpr_GetSpriteData(data_hsq2, 33);
	for(i=0;i<9;++i) spr_ihm_perso[i]= DuneSpr_GetSpriteData(data_hsq2, 65+i);
	i=0;

	liste_spr = DuneSpr_GetSpriteList(nom_spr_files[numero_spr_list]);
	
	sal_ptr = DuneSal_ChargeFichierSalle("dune/PALACE.SAL");

	sprintf(message,"scene = %d", numero_salle);
	PA_OutputText(1, 1, 3, message);

	while (1)
	{
		//PA_WaitForVBL();
		if (do_aff){
			PA_Clear8bitBg(0);

			DuneSal_AfficherSalle(sal_ptr, numero_salle , liste_spr, decalage_x,0);

			//DuneSpr_AfficherDataClip(spr_sable,1, 223+decalage_x, 66, 0);
			//DuneSpr_AfficherDataClip(spr_batiment,1, 0+decalage_x, 0, 0);
			//DuneSpr_AfficherDataClip(spr_rambarde,1, 0+decalage_x, 102, 0);
			//DuneSpr_AfficherDataClip(spr_entree,1, 223+decalage_x, 71, 0); 
			//DuneSpr_AfficherDataClip(spr_orni,1, 264+decalage_x, 127, 0);


			/*DuneSpr_AfficherDataClip(spr_colonne0,0, 188+decalage_x, 0, 0);
			DuneSpr_AfficherDataClip(spr_colonne0,1, 111+decalage_x, 0, 0);

			DuneSpr_AfficherDataClip(spr_salle,0, 6+decalage_x, 38, 0);
			DuneSpr_AfficherDataClip(spr_salle,1, 162+decalage_x, 38, 0);

			DuneSpr_AfficherDataClip(spr_toreau,0, 149+decalage_x, 3, 0);
			DuneSpr_AfficherDataClip(spr_toreau,1, 162+decalage_x, 3, 0);

			DuneSpr_AfficherDataClip(spr_sang,0, 148+decalage_x, 4, 0);
			DuneSpr_AfficherDataClip(spr_sang,1, 173+decalage_x, 4, 0);

			DuneSpr_AfficherDataClip(spr_porte1,0, 44+decalage_x, 40, 0);
			DuneSpr_AfficherDataClip(spr_porte2,0, 66+decalage_x, 40, 0);

			DuneSpr_AfficherDataClip(spr_porte1,1, 260+decalage_x, 40, 0);
			DuneSpr_AfficherDataClip(spr_porte2,1, 248+decalage_x, 40, 0);

			DuneSpr_AfficherDataClip(spr_sol,0, 0+decalage_x, 92, 0);

			DuneSpr_AfficherDataClip(spr_embleme,1, 48+decalage_x, 19, 0);
			DuneSpr_AfficherDataClip(spr_embleme,0, 256+decalage_x, 19, 0);

			DuneSpr_AfficherDataClip(spr_colonne1,1, 185+decalage_x, 0, 0);
			DuneSpr_AfficherDataClip(spr_colonne1,0, 135+decalage_x, 0, 0);

			DuneSpr_AfficherDataClip(spr_flag,0, 6+decalage_x, 0, 0);
			DuneSpr_AfficherDataClip(spr_flag,0, 88+decalage_x, 0, 0);
			DuneSpr_AfficherDataClip(spr_flag,0, 226+decalage_x, 0, 0);
			DuneSpr_AfficherDataClip(spr_flag,0, 307+decalage_x, 0, 0);

			DuneSpr_AfficherDataClip(spr_garde,0, 22+decalage_x, 58, 0);
			DuneSpr_AfficherDataClip(spr_garde,1, 283+decalage_x, 58, 0);

			DuneSpr_AfficherDataClip(spr_peau_garde,0, 27+decalage_x, 61, 0);
			DuneSpr_AfficherDataClip(spr_peau_garde,1, 285+decalage_x, 61, 0);*/

			DuneSpr_AfficherDataClip(spr_ihm_left,0, 0, 144, 0);
			DuneSpr_AfficherDataClip(spr_ihm_right,0, 164, 144, 0);

			DuneSpr_AfficherDataClip(spr_pierre_ihm,0, 2, 146, 0);
			DuneSpr_AfficherDataClip(spr_pierre_ihm,0, 253, 146, 0);

			DuneSpr_AfficherDataClip(spr_directions,0, 191, 154, 0);

			DuneSpr_AfficherDataClip(spr_ihm_perso[i],0, 35, 174, 0);


			//PA_WaitForVBL();

			//sprintf(message,"st_x1 = %d, st_x2 = %d    ",st_x1,st_x2);
			//PA_OutputText(1, 1, 1, message);
			PA_8bitSwapBuffer(0);
			do_aff=0;
		}

		//if ((Pad.Held.Right)&&(decalage_x>-64)) decalage_x-=2;
		//if ((Pad.Held.Left)&&(decalage_x<0)) decalage_x+=2;
		if (Pad.Newpress.Select){
			PA_Clear8bitBg(0);
			DuneSpr_AfficherPalette();
			PA_WaitForVBL();
			PA_8bitSwapBuffer(0);
			while(1){
				PA_WaitForVBL();
				if (Pad.Newpress.Select) {
					do_aff=1;
					break;
				}
			}
		}
		if (Pad.Newpress.Start){
			do_aff=1;
			//i++;
			//if (i==9) i=0;
			numero_salle++;
			if (numero_salle == 15) numero_salle=0;

			sprintf(message,"scene = %d", numero_salle);
			PA_OutputText(1, 1, 3, message);
		}

		if (Pad.Newpress.A){
			do_aff=1;
			DuneSpr_FreeSpriteList(liste_spr);
			++numero_spr_list; if (numero_spr_list == 8) numero_spr_list = 0;
			liste_spr = DuneSpr_GetSpriteList(nom_spr_files[numero_spr_list]);
		}
		/*if (Stylus.Newpress) {st_x2=st_x1=Stylus.X; decalage_x1=decalage_x; }*/
		if (Stylus.Held) {
			st_x2=Stylus.X; 
			decalage_x=decalage_x1+((st_x2-st_x1)>>1);
			//st_x1=Stylus.X;
			if (decalage_x<-64) decalage_x=-64;
			if (decalage_x>0) decalage_x=0;
			do_aff=1;
		}
		else{
			decalage_x1=decalage_x;
			st_x1=st_x2=Stylus.X;
		}
	}

	


    AS_MP3Stop(); 
	
	return 0;
}













