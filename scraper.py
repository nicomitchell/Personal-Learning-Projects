from bs4 import BeautifulSoup
import urllib

pg = urllib.urlopen('http://web.stanford.edu/~zlotnick/TextAsData/Web_Scraping_with_Beautiful_Soup.html').read()
soup = BeautifulSoup(pg)

content = soup.find_all('p')
strList = list()

for i in content:
    strList.append(str(i.get_text))

for l in strList:
    l = l.replace('<p>','')
    l = l.replace('</p>','')
    print l

with open('list.txt','w') as f:
    for i in strList:
        f.write(i)
        print "done"