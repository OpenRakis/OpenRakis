#ifndef DUNESPR_H
#define DUNESPR_H

#include <PA9.h>
#include "unhsq.h"


typedef struct {
	
  byte* data;
  int taille_x;
  int taille_y;
  byte offset_pal;
  
} ST_DUNESPR;

typedef struct {
	
  int nbr;
  ST_DUNESPR *sprite[100];
  
} ST_DUNESPR_LIST;

void DuneSpr_LoadPalette(ST_UNHSQ* hsq);
void DuneSpr_AfficherPalette(void);
int DuneSpr_CompterSprites(ST_UNHSQ* hsq);
void DuneSpr_AfficherSprite(ST_UNHSQ* hsq, int n, int flip, int x, int y, u8 screen);
void DuneSpr_AfficherSpriteClip(ST_UNHSQ* hsq, int n, int flip, int x, int y, u8 screen);
ST_DUNESPR* DuneSpr_GetSpriteData(ST_UNHSQ* hsq, int n);
void DuneSpr_AfficherDataClip(ST_DUNESPR* spr,int flip, int x, int y, u8 screen);
void DuneSpr_FreeSpriteData(ST_DUNESPR* spr);
ST_DUNESPR_LIST *DuneSpr_GetSpriteList(char *nom);
void DuneSpr_FreeSpriteList(ST_DUNESPR_LIST *list);



#endif

