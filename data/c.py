import mysql.connector as dbcn
import time
import datetime
from datetime import datetime as dt 
from random import randint , uniform
from decimal import *
import pandas as pd

print(dbcn)

s  = dt.now()


print(s.timestamp())

print(dt.fromtimestamp(s.timestamp()+1e+8))

low = int(s.timestamp() - 3e+8)
high = int(s.timestamp()) #+1e+8

Format='%Y-%M-%D %H:%M:%S'

print(dt.fromtimestamp(low).strftime(Format))

Query = lambda table , cname , val , id_colname , id : f'update {table} set {cname} = {val} where {id_colname} = {id}'

cnx = dbcn.connect(user = 'iee2019067',host='dblabs.iee.ihu.gr', 
                   database = 'iee2019067' ,password ='<>')

crs = cnx.cursor(prepared=True)


import numpy as np

ms = np.array(pd.read_csv('db.csv'))

for i in range(15164):
    if ms[i,4] != 0 and ms[i,4] <= 1021:
       crs.execute('select LecturerName from lecturer '
                f'where idLecturer = {ms[i,4]}')
       ms[i,4] = crs.fetchall()[0][0]
    else:
        ms[i,4] = "thug shaker"

pd.DataFrame(ms).to_csv('final.csv',index=False)

exit()
for i in range(crs.fetchall()[0][0]):
    r = uniform(2.2 , 4.8)
    crs.execute(Query('`modules`','`Rating`',Decimal(str(r)),'`idModules`',f'{i}'))

cnx.commit()

cnx.close()

