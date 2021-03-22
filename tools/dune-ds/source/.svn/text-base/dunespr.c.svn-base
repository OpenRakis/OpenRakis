
#include <PA9.h>

#include "dunespr.h"
#include "commun.h"

static char message[100];

ST_DUNESPR_LIST *DuneSpr_GetSpriteList(char *nom)
{
	ST_DUNESPR_LIST *liste = NULL;
	ST_UNHSQ* hsq = NULL;
	int i,j;

	liste = malloc(sizeof(ST_DUNESPR_LIST));
	if (!liste){
		return NULL;
	}

	hsq = unhsq_getdata(nom);
	if (!hsq){
		free(liste);
		return NULL;
	}

	DuneSpr_LoadPalette(hsq);

	liste->nbr = DuneSpr_CompterSprites(hsq);
	sprintf(message,"nbr_sprites carges = [%d]",liste->nbr);
	PA_OutputText(1, 1, 6, message);

	for (i=0; i<liste->nbr; i++){
		liste->sprite[i] = DuneSpr_GetSpriteData(hsq,i);
		if (!liste->sprite[i]) break;
	}

	if (i<liste->nbr) {
		for(j=0; j<i; j++) DuneSpr_FreeSpriteData(liste->sprite[j]);
		free(liste->sprite[j]);
		unhsq_freedata(hsq);
		return NULL;
	}

	unhsq_freedata(hsq);
	return liste;
}

void DuneSpr_FreeSpriteList(ST_DUNESPR_LIST *list)
{
	int i;

	for (i=0; i<list->nbr; ++i){
		DuneSpr_FreeSpriteData(list->sprite[i]);
	}
	free(list);
}

void DuneSpr_FreeSpriteData(ST_DUNESPR* spr)
{
	if (spr){
		if(spr->data) free(spr->data);
		free(spr);
		spr=NULL;
	}
}

ST_DUNESPR* DuneSpr_GetSpriteData(ST_UNHSQ* hsq, int n)
{
	byte* ptr_lec;
	byte* debut_offsets;
	byte* debut_sprite;
	byte* fin_sprite;
	byte* data_sortie;	
	ST_DUNESPR* spr;
	
	int taille_x, taille_y;
	byte compression;
	byte pal_offset;
	int ligne,colonne;
	signed char repetition;
	byte bipixel, bipixel2;
	int alignement;

	byte inconnu1,inconnu2;
	int j;
	

	if ( n >= DuneSpr_CompterSprites(hsq) ) return NULL;

	debut_offsets = hsq->data + *(hsq->data) + ((*(hsq->data+1))<<8);
	debut_sprite = debut_offsets + *(debut_offsets+(2*n)) + ((*(debut_offsets +(2*n)+ 1))<<8);
	
	ptr_lec = debut_sprite;
	taille_x = *ptr_lec++;
	compression = (*ptr_lec) & 0x80;
	taille_x += ((*ptr_lec++) & 0x7F)<<8;
	taille_y = *ptr_lec++;
	pal_offset = *ptr_lec++;

	if (debut_offsets == hsq->data + 2)
    {
		//inconnu1 = *ptr_lec++;
		//inconnu2 = *ptr_lec++;
	}

	//TODO gere les tailles nulles
	
	//TODO verif over read 
	
	ligne=colonne=0;
	alignement=0;
	data_sortie = malloc(taille_x * taille_y);
	if (!data_sortie) {
		return NULL;
	}

	spr=malloc(sizeof(ST_DUNESPR));
	if (!spr){
		free(data_sortie);
		return NULL;
	}

	spr->taille_x = taille_x;
	spr->taille_y = taille_y;
	spr->offset_pal = pal_offset;
	spr->data = data_sortie;

	if (n==22){sprintf(message,"taille_x = %d, taille_y = %d",spr->taille_x,spr->taille_y);
	PA_OutputText(1, 1, 1, message);}
	
	if (compression)
	{
		while(1)
		{
			repetition = *ptr_lec++;
			if(repetition < 0)
			{
				bipixel = *ptr_lec++;
				for(j=0; j< (-repetition)+1; ++j)
				{
					if (colonne<taille_x)
					{
						// PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel & 0x0F));
						*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel & 0x0F);
					}
					colonne++;
					alignement++;
				
					if (colonne<taille_x)
					{           
                	    //PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel>>4)); 
						*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel>>4);

                	}
					colonne++;  
					alignement++; 
				}
			
				if(colonne>=taille_x) 
				{ 
					colonne = 0; 
					ligne++;
					ptr_lec += (alignement%4)?4-(alignement%4):0; 
					alignement=0; 
				}  
			}
			else
			{
				for (j=0;j<repetition+1;++j)
				{ 
					bipixel = *ptr_lec++;
					
					if (colonne<taille_x)
					{
                    	// PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel & 0x0F));
						*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel & 0x0F);
                    }
                    colonne++;
                    alignement++;
                    
                    if (colonne<taille_x)
                    {
                    	//PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel>>4)); 
						*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel>>4);
                	}
                    colonne++;
                    alignement++;
				}
				
				if(colonne>=taille_x) 
				{ 
					colonne = 0; 
					ligne++;
                    ptr_lec += (alignement%4)?4-(alignement%4):0; 
                    alignement=0;
				}  
			}
			
			if (ligne>=taille_y ) break;
				
		}
	}
	else
	{
		while(1)
		{
			bipixel = *ptr_lec++;
			bipixel2 = *ptr_lec++;
			
			if (colonne<taille_x)
			{
				//PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel & 0x0F));
				*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel & 0x0F);
			}
			colonne++;
			
			if (colonne<taille_x)
			{           
				//PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel>>4));  
				*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel>>4);
			}
			colonne++; 
			
			if (colonne<taille_x)
			{
				//PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel2 & 0x0F));
				*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel2 & 0x0F);
			}
			colonne++;
			
			if (colonne<taille_x) 
			{           
				//PLOT(screen,colonne+x,ligne+y, offset_pal + (bipixel2>>4));  
				*(data_sortie+colonne+(ligne*taille_x)) = pal_offset + (bipixel2>>4);
			}
			colonne++; 
			
			if(colonne>=taille_x)
			{  
				colonne = 0;
				ligne++; 
			} 
			
			if (ligne>=taille_y ) break;
		}
	} 

	//sprintf(message,"taille_x = %d, taille_y = %d",spr->taille_x,spr->taille_y);
	//PA_OutputText(1, 1, 1, message);
	return(spr);
}


void DuneSpr_AfficherSprite(ST_UNHSQ* hsq, int n, int flip, int x, int y, u8 screen)
{
	byte* ptr_lec;
	byte* debut_offsets;
	byte* debut_sprite;
	byte* fin_sprite;
	
	int taille_x, taille_y;
	byte compression;
	byte offset_pal;
	int ligne,colonne;
	signed char repetition;
	byte bipixel, bipixel2;
	int alignement;
	byte inconnu1, inconnu2;
	
	int j;

	if ( n >= DuneSpr_CompterSprites(hsq) ) {
		PA_OutputText(1, 1, 1, "num sprite incorrect !!!");
		return;
	}

	debut_offsets = hsq->data + *(hsq->data) + ((*(hsq->data+1))<<8);
	debut_sprite = debut_offsets + *(debut_offsets+(2*n)) + ((*(debut_offsets +(2*n)+ 1))<<8);
	
	ptr_lec = debut_sprite;
	taille_x = *ptr_lec++;
	compression = (*ptr_lec) & 0x80;
	taille_x += ((*ptr_lec++) & 0x7F)<<8;
	taille_y = *ptr_lec++;
	offset_pal = *ptr_lec++;

	if (debut_offsets == hsq->data + 2)
    {
		inconnu1 = *ptr_lec++;
		inconnu2 = *ptr_lec++;
	}
	
	//TODO gere les tailles nulles
	sprintf(message,"taille_x = %d, taille_y = %d",taille_x,taille_y);
	PA_OutputText(1, 1, 1, message);
	
	ligne=colonne=0;
	alignement=0;
	
	if (compression)
	{
		while(1)
		{
			repetition = *ptr_lec++;
			if(repetition < 0)
			{
				bipixel = *ptr_lec++;
				for(j=0; j< (-repetition)+1; ++j)
				{
					if ((colonne<taille_x)&&(bipixel & 0x0F))
					{
						PA_Put8bitPixel (screen, (flip?(taille_x-1)-colonne:colonne) +x,ligne+y, offset_pal + (bipixel & 0x0F));
					}
					colonne++;
					alignement++;
				
					if ((colonne<taille_x)&&(bipixel>>4))
					{            
						PA_Put8bitPixel (screen, (flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel>>4));
                	}
					colonne++;  
					alignement++; 
				}
			
				if(colonne>=taille_x) 
				{ 
					colonne = 0; 
					ligne++;
					ptr_lec += (alignement%4)?4-(alignement%4):0; 
					alignement=0; 
				}  
			}
			else
			{
				for (j=0;j<repetition+1;++j)
				{ 
					bipixel = *ptr_lec++;
					
					if ((colonne<taille_x)&&(bipixel & 0x0F))
					{
                    	PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel & 0x0F));
                    }
                    colonne++;
                    alignement++;
                    
                    if ((colonne<taille_x)&&(bipixel>>4))
                    {
                    	PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel>>4)); 
                	}
                    colonne++;
                    alignement++;
				}
				
				if(colonne>=taille_x) 
				{ 
					colonne = 0; 
					ligne++;
                    ptr_lec += (alignement%4)?4-(alignement%4):0; 
                    alignement=0;
				}  
			}
			
			if (ligne>=taille_y ) break;
				
		}
	}
	else
	{
		while(1)
		{
			bipixel = *ptr_lec++;
			bipixel2 = *ptr_lec++;
			
			if ((colonne<taille_x)&&(bipixel & 0x0F))
			{
				PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel & 0x0F));
			}
			colonne++;
			
			if ((colonne<taille_x)&&(bipixel>>4))
			{           
				PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel>>4));  
			}
			colonne++; 
			
			if ((colonne<taille_x)&&(bipixel2 & 0x0F))
			{
				PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel2 & 0x0F));
			}
			colonne++;
			
			if ((colonne<taille_x)&&(bipixel2>>4)) 
			{           
				PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel2>>4));  
			}
			colonne++; 
			
			if(colonne>=taille_x)
			{  
				colonne = 0;
				ligne++; 
			} 
			
			if (ligne>=taille_y ) break;
		}
	}

}

void DuneSpr_AfficherDataClip(ST_DUNESPR* spr,int flip, int x, int y, u8 screen)
{
	byte pixel;
	int i, j;

	//sprintf(message,"taille_x = %d, taille_y = %d",spr->taille_x,spr->taille_y);
	//PA_OutputText(1, 1, 1, message);

	for(j=0;j<spr->taille_y;++j)
	{
		for(i=0;i<spr->taille_x;++i)
		{
			pixel = *(spr->data+i+(j*spr->taille_x));
			if (pixel != spr->offset_pal)
			if (((flip?(spr->taille_x-1)-i:i)+x>=0)&&((flip?(spr->taille_x-1)-i:i)+x<256)&&(j+y>=0)&&(j+y<192))
				PA_Put8bitPixel (screen, (flip?(spr->taille_x-1)-i:i)+x,j+y, pixel);
		}
	}
}

void DuneSpr_AfficherSpriteClip(ST_UNHSQ* hsq, int n, int flip, int x, int y, u8 screen)
{
	byte* ptr_lec;
	byte* debut_offsets;
	byte* debut_sprite;
	byte* fin_sprite;
	
	int taille_x, taille_y;
	byte compression;
	byte offset_pal;
	int ligne,colonne;
	signed char repetition;
	byte bipixel, bipixel2;
	int alignement;

	byte inconnu1,inconnu2;
	
	int j;

	if ( n >= DuneSpr_CompterSprites(hsq) ) {
		PA_OutputText(1, 1, 1, "num sprite incorrect !!!");
		return;
	}

	debut_offsets = hsq->data + *(hsq->data) + ((*(hsq->data+1))<<8);
	debut_sprite = debut_offsets + *(debut_offsets+(2*n)) + ((*(debut_offsets +(2*n)+ 1))<<8);
	
	ptr_lec = debut_sprite;
	taille_x = *ptr_lec++;
	compression = (*ptr_lec) & 0x80;
	taille_x += ((*ptr_lec++) & 0x7F)<<8;
	taille_y = *ptr_lec++;
	offset_pal = *ptr_lec++;

	if (debut_offsets == hsq->data + 2)
    {
		inconnu1 = *ptr_lec++;
		inconnu2 = *ptr_lec++;
	}
	
	//TODO gere les tailles nulles

	ligne=colonne=0;
	alignement=0;
	
	if (compression)
	{
		while(1)
		{
			repetition = *ptr_lec++;
			if(repetition < 0)
			{
				bipixel = *ptr_lec++;
				for(j=0; j< (-repetition)+1; ++j)
				{
					if ((colonne<taille_x)&&(bipixel & 0x0F)&&((flip?(taille_x-1)-colonne:colonne)+x>=0)&&((flip?(taille_x-1)-colonne:colonne)+x<256)&&(ligne+y>=0)&&(ligne+y<192))
					{
						PA_Put8bitPixel (screen, (flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel & 0x0F));
					}
					colonne++;
					alignement++;
				
					if ((colonne<taille_x)&&(bipixel>>4)&&((flip?(taille_x-1)-colonne:colonne)+x>=0)&&((flip?(taille_x-1)-colonne:colonne)+x<256)&&(ligne+y>=0)&&(ligne+y<192))
					{            
						PA_Put8bitPixel (screen, (flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel>>4));
                	}
					colonne++;  
					alignement++; 
				}
			
				if(colonne>=taille_x) 
				{ 
					colonne = 0; 
					ligne++;
					ptr_lec += (alignement%4)?4-(alignement%4):0; 
					alignement=0; 
				}  
			}
			else
			{
				for (j=0;j<repetition+1;++j)
				{ 
					bipixel = *ptr_lec++;
					
					if ((colonne<taille_x)&&(bipixel & 0x0F)&&((flip?(taille_x-1)-colonne:colonne)+x>=0)&&((flip?(taille_x-1)-colonne:colonne)+x<256)&&(ligne+y>=0)&&(ligne+y<192))
					{
                    	PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel & 0x0F));
                    }
                    colonne++;
                    alignement++;
                    
                    if ((colonne<taille_x)&&(bipixel>>4)&&((flip?(taille_x-1)-colonne:colonne)+x>=0)&&((flip?(taille_x-1)-colonne:colonne)+x<256)&&(ligne+y>=0)&&(ligne+y<192))
                    {
                    	PA_Put8bitPixel (screen,(flip?(taille_x-1)-colonne:colonne)+x,ligne+y, offset_pal + (bipixel>>4)); 
                	}
                    colonne++;
                    alignement++;
				}
				
				if(colonne>=taille_x) 
				{ 
					colonne = 0; 
					ligne++;
                    ptr_lec += (alignement%4)?4-(alignement%4):0; 
                    alignement=0;
				}  
			}
			
			if (ligne>=taille_y ) break;
				
		}
	}
	else
	{
		while(1)
		{
			bipixel = *ptr_lec++;
			bipixel2 = *ptr_lec++;
			
			if ((colonne<taille_x)&&(bipixel & 0x0F)&&(colonne+x>=0)&&(colonne+x<256)&&(ligne+y>=0)&&(ligne+y<192))
			{
				PA_Put8bitPixel (screen,colonne+x,ligne+y, offset_pal + (bipixel & 0x0F));
			}
			colonne++;
			
			if ((colonne<taille_x)&&(bipixel>>4)&&(colonne+x>=0)&&(colonne+x<256)&&(ligne+y>=0)&&(ligne+y<192))
			{           
				PA_Put8bitPixel (screen,colonne+x,ligne+y, offset_pal + (bipixel>>4));  
			}
			colonne++; 
			
			if ((colonne<taille_x)&&(bipixel2 & 0x0F)&&(colonne+x>=0)&&(colonne+x<256)&&(ligne+y>=0)&&(ligne+y<192))
			{
				PA_Put8bitPixel (screen,colonne+x,ligne+y, offset_pal + (bipixel2 & 0x0F));
			}
			colonne++;
			
			if ((colonne<taille_x)&&(bipixel2>>4)&&(colonne+x>=0)&&(colonne+x<256)&&(ligne+y>=0)&&(ligne+y<192))
			{           
				PA_Put8bitPixel (screen,colonne+x,ligne+y, offset_pal + (bipixel2>>4));  
			}
			colonne++; 
			
			if(colonne>=taille_x)
			{  
				colonne = 0;
				ligne++; 
			} 
			
			if (ligne>=taille_y ) break;
		}
	}		 
}

int DuneSpr_CompterSprites(ST_UNHSQ* hsq)
{
	byte* debut_offsets;
	byte* debut_sprites;

	debut_offsets = hsq->data + *(hsq->data) + ((*(hsq->data+1))<<8);
	debut_sprites = debut_offsets + *debut_offsets + ((*(debut_offsets + 1))<<8);

	return((debut_sprites - debut_offsets) >>1);

}

void DuneSpr_LoadPalette(ST_UNHSQ* hsq)
{
	
	byte *bp_data;
	void* offset_fin_pal;
	byte debut_mes_couleur;
	byte nb_mes_couleur;
	byte r,g,b;


	int i;

	bp_data = hsq->data;

	offset_fin_pal = bp_data + (*bp_data) + ((*(bp_data+1))<<8);

	if (offset_fin_pal == (void*)(bp_data + 2)) {
		PA_OutputText(1, 1, 1, "no pal data !!!");
        return;
	}
	
	bp_data += 2;

	while ((void*)bp_data < offset_fin_pal) {
            debut_mes_couleur = *bp_data++;			
			nb_mes_couleur = *bp_data++;            			
            
            if( (debut_mes_couleur==0xFF) && (nb_mes_couleur==0xFF)) break;
        
            for(i=0;(i<nb_mes_couleur);i++){
			   r=*bp_data++;
			   g=*bp_data++;
			   b=*bp_data++;			  
			   PA_SetBgPalCol(0, debut_mes_couleur+i, PA_RGB(r>>1, g>>1, b>>1));
            }
    }
}



void DuneSpr_AfficherPalette(void)
{
	int i,x,y,z;
	i=0;
	for(y=0;y<16;++y)
	{
		for(x=0;x<16;++x)
		{
			for(z=0;z<12;++z) 
				PA_Draw8bitLine (0, x*12, ((y*12)+z), (x+1)*12, (y*12)+z, i);
			++i;
		}
	}
}
