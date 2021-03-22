#include <PA9.h>

#include "unhsq.h"
#include "commun.h"

static byte *ori_src;
static byte *ori_dst;

static char unhsq_unpack2(byte *src, byte *dst);
static int unhsq_getbit(unsigned int *q, byte** src);
static void* unhsq_loadfile(const char *filename);


static void* unhsq_loadfile(const char *filename)
{
	
       FILE* f = fopen(filename, "r");
       if (f == NULL) {
			return(NULL);
       }

       unsigned short size = 0;
       fseek(f, 3, SEEK_SET);
       fread(&size, 2,1,f);

       byte *data = malloc(size);
	   if(!data) return NULL;

       fseek(f, 0, SEEK_SET);
       fread(data, size,1,f);

       fclose(f);

       return data;
}

int unhsq_ishsq(char* filename){
	byte src[6];
	int i;

	FILE* f = fopen(filename, "r");
	if (f == NULL) {
		return(0);
	}

	for(i=0;i<6; fread(&src[i++], 1,1,f)); 
	fclose(f);

	if (((src[0] + src[1] + src[2] + src[3] + src[4] + src[5]) & 0xff) != 171) return 0;
	return 1;
}

static int unhsq_getbit(unsigned int *q, byte** src)
{
       if (*q == 1) {
				
               *q = 0x10000 | (**src)| ((*(*src+1))<<8);			   
               *src += 2;			   
       }
       if (*q & 1) {			
       			// q impair
               *q >>= 1;
               return 1;
       }
       else {				
       			// q paire
               *q >>= 1;
               return 0;
       }
}

static char unhsq_unpack2(byte *src, byte *dst)
{
		
       if (((src[0] + src[1] + src[2] + src[3] + src[4] + src[5]) & 0xff) != 171) return 0;
       unsigned int q = 1;

	   src += 6;

       while (1) {	
				if (unhsq_getbit(&q, &src)) {                  
					*dst++ = *src++;					
				}
               else {
                       int count;
                       int offset;
					   
                       if (unhsq_getbit(&q, &src)) {
	
                               count = *src & 7;					
                               offset = 0xffffe000 | ( ( (*src)| ((*(src+1))<<8))>>3);
                               src += 2;                  
                               if (!count) count = *src++;                               
                               if (!count)     return 1;
                       }
                       else {
                               count = unhsq_getbit(&q, &src) << 1;
                               count |= unhsq_getbit(&q, &src);
                               offset = 0xffffff00 | *src++;
                       }

                       count += 2;
                       byte *dm = dst + offset;

					   while (count--) {						   
						   *dst++ = *dm++;
					   }
               }
       }
}


ST_UNHSQ* unhsq_getdata(char* filename)
{
	ST_UNHSQ *ptr;
	byte *dst;
	byte *src;
	unsigned short size;
	
	src = unhsq_loadfile(filename);
	if (!src) return NULL;
	ori_src = src;

	size = *src + ((*(src+1))<<8);					
	
	dst = malloc(size);
	ori_dst = dst;
	if (!dst) {
		free(src);
		return(NULL);
	}
	
	ptr = malloc(sizeof(ST_UNHSQ));
	if (!ptr){		
		free(ori_dst);
		free(ori_src);	
		return(NULL);	
	}
	
	ptr->size = size;
	ptr->data = ori_dst;

	if (unhsq_unpack2(src, dst)) 
	{
		free(ori_src);
		return(ptr);
	}
	else {
		free(ori_src);
		free(ori_dst);
		free(ptr);
		return(NULL);
	}
}

void unhsq_freedata(ST_UNHSQ* hsq)
{
	if (hsq){
		if(hsq->data) free(hsq->data);
		free(hsq);
		hsq=NULL;
	}
}

