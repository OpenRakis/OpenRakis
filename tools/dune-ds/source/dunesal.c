#include <PA9.h>

#include "dunesal.h"
#include "commun.h"


static char message[100];

static void DuneSal_DrawLine(u8 screen, int x1, int y1, int x2, int y2, u8 color);

static int polyx[200][2]; //stores left & right edge coordinates of polygon
static char drawpoly(int np,int *iptr,u8 col, u8 screen);
static char pline(int x1,int y1,int x2,int y2);

ST_UNHSQ* DuneSal_ChargeFichierSalle(char *nom)
{
	byte *data = NULL;
	ST_UNHSQ *ptr = NULL;
	FILE* fp = NULL;
	int taille = 0;

	if (unhsq_ishsq(nom)){
		//PA_OutputText(1, 1, 2, "est HSQ");
		ptr = unhsq_getdata(nom);
		if (!ptr) {
			PA_OutputText(1, 1, 1, "erreur SAL #1#");
			return NULL;
		}
	}else{
		//PA_OutputText(1, 1, 2, "pas HSQ");
		fp = fopen(nom ,"r");
		if (!fp) return NULL;
		fseek(fp,0,SEEK_END);
		taille = ftell(fp);
		fseek(fp,0,SEEK_SET);

		data = malloc(taille);
		if (!data) {
			PA_OutputText(1, 1, 1, "erreur SAL #2#");
			return NULL;
		}

		ptr = malloc(sizeof(ST_UNHSQ));
		if (!ptr){
			PA_OutputText(1, 1, 1, "erreur SAL #3#");
			free(data);
			return NULL;
		}

		ptr->size = taille;
		ptr->data = data;
		fread(ptr->data, taille,1,fp);
	}
	return ptr;
}

int DuneSal_CompterSalle(ST_UNHSQ *ptr)
{
	byte *premiere_salle;
	
	//sprintf(message,"a=%02X, b=%02X",(byte)*(ptr->data),(byte)*(ptr->data+1));
	//PA_OutputText(1, 1, 1, message);

	premiere_salle = ptr->data + *(ptr->data) + ((*(ptr->data+1))<<8);
	return ((premiere_salle - ptr->data)>>1);
}

void DuneSal_AfficherSalle(ST_UNHSQ *ptr_sal, int n , ST_DUNESPR_LIST * spr_list, int decalage_x ,u8 screen)
{
	byte *offset_salle;
	byte *ptr_lec;
	byte nbr_marqueurs;
	byte nbr_sprite;
	byte modificator;
	byte x_sprite;
	byte y_sprite;
	byte pal_offset;
	int  decalage_x2;

	if (n >= DuneSal_CompterSalle(ptr_sal)) {
		PA_OutputText(1, 1, 1, "DuneSal_AfficherSalle:");
		PA_OutputText(1, 1, 2, "n trop grand");
		return;
	}

	ptr_lec = offset_salle = ptr_sal->data + *(ptr_sal->data+(2*n)) + ((*(ptr_sal->data +(2*n)+ 1))<<8);

	nbr_marqueurs = *ptr_lec++;

	while (1){
		nbr_sprite = *ptr_lec++;
		modificator = *ptr_lec++;

		if ((nbr_sprite==0xFF)&&(modificator==0xFF)) break;
		else if (modificator & 0x80){
			if (modificator & 0x40) {
				// ligne
				u16 x1,y1,x2,y2;
				x1 = *ptr_lec++;
				x1 += ((*ptr_lec++)&0x0F)<<8;
				y1 = *ptr_lec++;
				y1 += ((*ptr_lec++)&0x0F)<<8;
				x2 = *ptr_lec++;
				x2 += ((*ptr_lec++)&0x0F)<<8;
				y2 = *ptr_lec++;
				y2 += ((*ptr_lec++)&0x0F)<<8;
				//ptr_lec+=8;
				DuneSal_DrawLine(screen, x1+decalage_x, y1, x2+decalage_x, y2, nbr_sprite);
			}else{
				
				// polygone
				byte fin_forme =0;
				int np = 0;
				int points[4][2];

				ptr_lec+=2;
				//strcpy(message,"");

				while(fin_forme < 0xC0){
					points[np][0] = *ptr_lec++;
					points[np][0] += ((*ptr_lec)&0x0F)<<8;
					points[np][0] += decalage_x;
					fin_forme += ((*ptr_lec++)& 0xF0);					
					points[np][1] = *ptr_lec++;
					points[np][1] += ((*ptr_lec++)&0x0F)<<8;
					//sprintf(message,"%s %d,%d",message,points[np][0]-decalage_x,points[np][1]);
					++np;				
				}
				//ptr_lec+=3;
				//PA_OutputText(1, 1, 8, message);
				//PA_WaitFor(Pad.Newpress.B);

				drawpoly(np, *points,nbr_sprite,screen);
			}
		}
		else if (nbr_sprite == 0x01){
			// marqueur			
			ptr_lec+=3;
		}
		//else if ((nbr_sprite > 0x01) && (! (modificator & 0x80)) && (! (modificator & 0x40)))
		else if ((nbr_sprite > 0x01) /*&& (! (nbr_sprite & 0x80))*/ )
		{
			// sprite normal
			x_sprite = *ptr_lec++;
			y_sprite = *ptr_lec++;
			pal_offset = *ptr_lec++;

			if (modificator & 0x02) decalage_x2 = 256;
			else decalage_x2 = 0;

			if (nbr_sprite-1 < spr_list->nbr) 
			DuneSpr_AfficherDataClip(spr_list->sprite[nbr_sprite-1],modificator & 0x40, x_sprite+decalage_x+decalage_x2, y_sprite, screen);
		}
		/*else if ((nbr_sprite == 0x01) && (! (modificator & 0x80))){
			// marqueur
			ptr_lec+=3;
		}else{
			// forme
			ptr_lec+=18;
		}*/
	}
}


static void DuneSal_DrawLine(u8 screen, int x1, int y1, int x2, int y2, u8 color){
  int i,dx,dy,sdx,sdy,dxabs,dyabs,x,y,px,py;

  dx=x2-x1;      /* the horizontal distance of the line */
  dy=y2-y1;      /* the vertical distance of the line */
  dxabs = dx;
  sdx = 1;
  if (dx < 0) {
	  dxabs = -dx;
	  sdx = -1;
  }
  dyabs = dy;
  sdy = 1;
  if (dy < 0) {
	  dyabs = -dy;
	  sdy = -1;
  }

  x=dyabs>>1;
  y=dxabs>>1;
  px=x1;
  py=y1;
  
  //if (x<256)
  //PA_Put8bitPixel(screen, px, py, color);


  if (dxabs>=dyabs) {
    for(i=0;i<dxabs;i++)  {
      y+=dyabs;
      if (y>=dxabs)  {
        y-=dxabs;
        py+=sdy;
      }
      px+=sdx;
	  if ((px<256)&&(px>=0))
      PA_Put8bitPixel(screen, px, py, color);
    }
  }
  else {
    for(i=0;i<dyabs;i++) {
      x+=dxabs;
      if (x>=dyabs)  {
        x-=dyabs;
        px+=sdx;
      }
      py+=sdy;
	  if ((px<256)&&(px>=0))
      PA_Put8bitPixel(screen, px, py, color);
    }
  }
}


static char drawpoly(int np,int *iptr,u8 col, u8 screen)
{
	int top=199,bottom=0;
	int a,b,x1,y1,ligne;	for(a=0;a<200;++a) {polyx[a][0]= 320; polyx[a][1]= 0; }
	//go through polygon lines and calculate x values
	np--;
	for(a=0; a<np; a++) 
	{
		//do up to last line
		b=a*2;
		x1=iptr[b]; y1=iptr[b+1];
		if (y1<top) top=y1;
		if (y1>bottom) bottom=y1;
		pline(x1,y1,iptr[b+2],iptr[b+3]);
	}

	//do last line
	x1=iptr[np*2]; y1=iptr[(np*2)+1];
	if (y1<top) top=y1;
	if (y1>bottom) bottom=y1;
	pline(x1,y1,iptr[0],iptr[1]);
	if (bottom<top) return(0);//error

	//jbi vga=vgascreen+(top*320);	ligne = top;
	//loop from top to bottom of polygon, drawing lines
	for(a=top; a<bottom+1; a++)
	{
		if (polyx[a][0] > polyx[a][1]) 
		{
			++ligne;
			continue;
		}//error

		//jbi memset(vga+polyx[a][0], col, polyx[a][1]-polyx[a][0]+1);
		DuneSal_DrawLine(screen, polyx[a][0], ligne, polyx[a][1], ligne, col);

		//jbi vga=vga+320;
		++ligne;
	}
	return(1);
}

static char pline(int x1,int y1,int x2,int y2)
{
	char xu;
	//int offset;
	int yd=y2-y1,yd2;
	int xd=x2-x1;
	int err=0;
	int X=x1,Y;
	if (y1==y2) return(0);//horizontal line
	if (yd<0) yd=-yd;
	if (xd<0) {
		xd=-xd; 
		xu=-1;
	}
	else xu=1;
	//this line makes some edge lines better
	//not essential
	if (yd>xd) err=yd>>1;
	//record x values along line
	yd2=yd-1;
	if (xu==1) {
		if (y1>y2) {
			for (Y=y1; Y>y2-1; Y--)
			{
				//jbi polyx[Y][0]=X;
				if (X< polyx[Y][0]) polyx[Y][0]=X;
				if (X> polyx[Y][1]) polyx[Y][1]=X;
				err+=xd;
				if (err>yd2) 
				do{
					err-=yd; 
					X++;
				}while(err>=yd);
			}
			return(0);
		}else{
			for (Y=y1; Y<y2+1; Y++)
			{
				//jbi polyx[Y][1]=X;
				if (X< polyx[Y][0]) polyx[Y][0]=X;
				if (X> polyx[Y][1]) polyx[Y][1]=X;
				err+=xd;

				if (err>yd2) 
				do{
					err-=yd; 
					X++;
				}while(err>=yd);
			}
			return(0);
		}
	}else {
		if (y1>y2) {
			for (Y=y1; Y>y2-1; Y--)
			{
				//jbi polyx[Y][0]=X;
				if (X< polyx[Y][0]) polyx[Y][0]=X;
				if (X> polyx[Y][1]) polyx[Y][1]=X;
				err+=xd;
				if (err>yd2) 				do{					err-=yd; 					X--;				}				while(err>=yd);
			}
			return(0);
		}else {
			for (Y=y1; Y<y2+1; Y++)
			{
				//jbi polyx[Y][1]=X;
				if (X< polyx[Y][0]) polyx[Y][0]=X;
				if (X> polyx[Y][1]) polyx[Y][1]=X;
				err+=xd;

				if (err>yd2) 
				do{
					err-=yd; 
					X--;
				}
				while(err>=yd);
			}
		}
	}
	return(0);
}





