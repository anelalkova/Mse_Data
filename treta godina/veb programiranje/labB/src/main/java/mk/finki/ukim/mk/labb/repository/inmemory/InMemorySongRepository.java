package mk.finki.ukim.mk.labb.repository;

import mk.finki.ukim.mk.labb.bootstrap.DataHolder;
import mk.finki.ukim.mk.labb.model.*;
import org.springframework.stereotype.Repository;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Repository
public class InMemorySongRepository {

    public List<Song> findAll(){
        return DataHolder.songs;
    }

    public List<Song> listByCategory(String name){
        return DataHolder.songs.stream()
                .filter(c->c.getCategory().name.equals(name))
                .collect(Collectors.toList());
    }

    public Song findByTrackId(Long trackId){
        return DataHolder.songs.stream()
                .filter(c->c.trackId.equals(trackId))
                .findFirst().orElse(new Song("N","N",2000,DataHolder.artists,new Category("cat1"),DataHolder.albums.get(0)));
    }

    public Artist addArtistToSong(Artist artist, Song song){
        Artist a = DataHolder.artists
                .stream().filter(c-> c.getId().equals(artist.getId()))
                .findFirst().orElse(DataHolder.artists.get(0));

        DataHolder.songs.stream().filter(c->c.getTrackId().equals(song.getTrackId()))
                .findFirst().ifPresent(c->c.getPerformers().add(a));

        return a;

    }

//    public void delete(String title){
//        DataHolder.songs.removeIf(c->c.getTitle().equals(title));
//    }
//    public void deleteByTrackId(String trackId){
//        DataHolder.songs.removeIf(c->c.getTrackId().equals(trackId));
//    }
//    public void deleteById(Long id){
//        DataHolder.songs.removeIf(c->c.trackId.equals(id));
//    }
//    public Optional<Song> save(Song song){
//        DataHolder.songs.removeIf(c->c.getTitle().equals(song.getTitle()));
//        DataHolder.songs.add(song);
//        return Optional.of(song);
//    }

    public Optional<Song> save(Long trackId,String title,String genre,
                               int releaseYear, Long albumId){

        List<Artist> performers = new ArrayList<>();
        Category category = new Category("name");
        Song s = new Song(title,genre,releaseYear,performers,category,DataHolder.albums.stream().filter(a -> a.getId().equals(albumId)).findFirst().orElse(null));
        DataHolder.songs.removeIf(l ->l.getTitle().equals(title));
        DataHolder.songs.add(s);
        return Optional.of(s);
    }

    public void deleteSong(Long id){
        DataHolder.songs.removeIf(c->c.trackId.equals(id));
    }


    public void addReview(Review r,Song s){
        s.getReviews().add(r);
    }


}
